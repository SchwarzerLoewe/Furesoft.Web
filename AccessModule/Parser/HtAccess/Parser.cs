using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Furesoft.Web;

namespace AccessModule.Parser.HtAccess
{
    public class Parser
    {
        public Stack<Furesoft.Web.Internal.HtAccess.ICommand> commands = new Stack<Furesoft.Web.Internal.HtAccess.ICommand>();

        private Dictionary<string, string> _contants = new Dictionary<string, string>();

        public void AddContstant(string name, string value)
        {
            _contants.Add(name, value);
        }

        public void PopulateCondition(string condition, object o)
        {
            foreach (var item in commands.OfType<Furesoft.Web.Internal.HtAccess.Block>())
            {
                if (item.Name.ToLower() == "if")
                {
                    if (EvalCondition(item.Argument, condition))
                    {
                        Populate(o);
                    }
                }
            }
        }

        private bool EvalCondition(string value, string con)
        {
            return Regex.IsMatch(value, con);
        }

        public void Populate(object o)
        {
            var t = o.GetType();
            foreach (var item in commands)
            {
                foreach (var prop in t.GetFields())
                {
                    if (prop.Name == item.Name)
                    {
                        var p = item as Furesoft.Web.Internal.HtAccess.Directive;
                        if (prop.IsPublic)
                        {
                            if (prop.FieldType.Name == typeof(List<string>).Name)
                            {
                                var v = (List<string>)prop.GetValue(o);

                                v.AddRange(p.Values.Cast<string>());

                                prop.SetValue(o, v);
                            }
                            else if (prop.FieldType.Name == typeof(bool).Name)
                            {
                                bool b = false;

                                if (p.Values[0].ToString().ToLower() == "true" || p.Values[0].ToString().ToLower() == "on")
                                {
                                    b = true;
                                }
                                else if (p.Values[0].ToString().ToLower() == "false" || p.Values[0].ToString().ToLower() == "off")
                                {
                                    b = false;
                                }

                                prop.SetValue(o, b);
                            }
                            else if (prop.FieldType.Name == typeof(Dictionary<string, string>).Name)
                            {
                                var v = (Dictionary<string, string>)prop.GetValue(o);

                                v.Add((string)p.Values[0], (string)p.Values[1]);

                                prop.SetValue(o, v);
                            }
                            else
                            {
                                prop.SetValue(o, p.Values[0]);
                            }
                        }
                    }
                }
            }
        }

        public T GetCommand<T>(string name) where T : Furesoft.Web.Internal.HtAccess.ICommand
        {
            foreach (var item in commands)
            {
                if (item.Name == name)
                {
                    return (T)item;
                }
            }
            return default(T);
        }

        public void Parse(string content)
        {
            Furesoft.Web.Internal.HtAccess.Block block = null;
            bool incomment = false;

            foreach (var c in _contants)
            {
                content = content.Replace("%{" + c.Key + "}", c.Value);
            }

            foreach (var line in Utils.Split(content, '\r', '"'))
            {
                var tline = line.Trim();
                if (tline.StartsWith("#"))
                {
                    continue;
                }
                else if (tline.StartsWith("/*"))
                {
                    incomment = true;
                }
                else if (tline.StartsWith("*/"))
                {
                    incomment = false;
                }
                else if (!incomment)
                {
                    if (tline.StartsWith("<"))
                    {
                        if (tline[1] == '/')
                        {
                            var l = tline.Remove(0, 1);
                            var name = l.Substring(1, l.LastIndexOf('>') - 1);
                            if (name != block.Name)
                            {
                                throw new Exception("Not Knowed Ending Block");
                            }
                            else
                            {
                                commands.Push(block);

                                block = null;
                            }
                        }
                        else
                        {
                            block = new Furesoft.Web.Internal.HtAccess.Block();

                            var intag = tline.Substring(1, tline.LastIndexOf('>') - 1).Split(' ');
                            block.Name = intag[0];
                            block.Argument = intag[1];
                        }
                    }
                    else
                    {
                        var lspl = Utils.Split(tline, ' ', '"').ToList();
                        var dir = new Furesoft.Web.Internal.HtAccess.Directive();
                        dir.Name = lspl[0];
                        lspl.RemoveAt(0);

                        var args = new List<object>();

                        foreach (var item in lspl.ToArray())
                        {
                            if (item == "on" | item == "true")
                            {
                                args.Add(true);
                            }
                            else if (item == "off" | item == "false")
                            {
                                args.Add(false);
                            }
                            else
                            {
                                args.Add(item.TrimStart('"').TrimEnd('"'));
                            }
                        }

                        dir.Values = args.ToArray();

                        if (block != null)
                        {
                            block.Directives.Add(dir);
                        }
                        else
                        {
                            commands.Push(dir);
                        }
                    }
                }
            }
        }
    }
}