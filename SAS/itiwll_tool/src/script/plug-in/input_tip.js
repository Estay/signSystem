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
				tip_text = $(this).attr("tip_text"),
				text = tip_text ? tip_text : text;
			el.addClass('col_gray');
			el.val(text);
			el.focusin(function(event) {
				if (el.val()==text) {
					el.val("").removeClass('col_gray');
				};
			})
			.focusout(function(event) {
				if(el.val()==""){
					el.val(text).addClass('col_gray');
				}
			})
			.keyup(function(event) {
				if (el.val()==text) {
					el.addClass('col_gray');
				}else {
					el.removeClass('col_gray');
				};
			});
		});
	}
})(jQuery);