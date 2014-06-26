// e_tab_switch tab切换插件
// 参数options
// 		tab_class 	tab类名
// 		box_class 	box类名
// 		callback	切换后的回调函数 
(function($) {
	$.fn.e_tab_switch = function(options) {

		if (!this.length) {
			return this;
		}

		var opts = $.extend(true, {}, $.fn.e_tab_switch.defaults, options);

		this.each(function() {
			var $this = $(this),
				el_tabs = $this.find("." + opts.tab_class),
				el_boxs = $this.find("." + opts.box_class);

			el_tabs.click(function(event) {
				event.preventDefault();
				var index = el_tabs.index($(this));
				$(this).addClass('set');
				el_tabs.not($(this)).removeClass("set");
				el_boxs.hide().eq(index).show();
				if(opts.callback){
					opts.callback();
				}
			});
		});

		return this;
	};

	// default options
	$.fn.e_tab_switch.defaults = {
		tab_class: "e_tab",
		box_class: "e_tab_box",
		callback: null
	};

})(jQuery);