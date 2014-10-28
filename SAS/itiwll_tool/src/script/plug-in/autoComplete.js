(function($) {
// 输入提示插件
$.fn.autoComplete = function(options) {

  if (!this.length) { return this; }

  var opts = $.extend(true, {}, $.fn.autoComplete.defaults, options);

  this.each(function() {
    var $this = $(this),
    	timeout;
    $this.keyup(function(event) {
	    clearTimeout(timeout);
    	if($this != ""){		
	    		timeout = setTimeout(function() {
	    		opts.callblack.call($this[0],$this.val());
	    	}, opts.time);
    	}
    });
    
  });

  return this;
};

// default options
$.fn.autoComplete.defaults = {
  time : 500,
  // url : "",
  callblack : function(text) {
  	console.log("已输入"+text);
  }
};

})(jQuery);
