/********************************************************
 ** jQuery itiwll_input_tip
 ** 20140623
 **
 **********************************************************/
(function($) {
	$.fn.e_input_tip = function(options) {
		var defaults = "请输入";
			return this.each(function() {
				var el = $(this);
				var text = options ? options : defaults,
					attr_data = $(this).attr("tip_text");
				text = attr_data ? attr_data : text;
				el.addClass('col_gray');
				el.val(text);
			});
	}
})(jQuery);