// e_tab_switch tab切换插件
// 参数options
// 		tab_class 	tab类名
// 		box_class 	box类名
// 		callback	切换后的回调函数 
(function($) {
$.fn.e_tab_switch = function(options) {

  if (!this.length) { return this; }

  var opts = $.extend(true, {}, $.fn.e_tab_switch.defaults, options);

  this.each(function() {
    var $this = $(this),
    index = $this.index("."+opts.tab_class),
    el_boxs = $("."+opts.box_class_box);
    $this.addClass('set');
    $("."+opts.tab_class).not($this).remove("set");
    el_boxs.hide().eq(index).show();
  });

  return this;
};

// default options
$.fn.e_tab_switch.defaults = {
  tab_class: "e_tab",
  box_class: "box_class_box",
  callback: null
};

})(jQuery);

