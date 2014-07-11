/********************************************************
 ** jQuery itiwll_input_tip
 ** 20140623
 **
 **********************************************************/
(function($) {
	$.fn.e_input_tip = function(options) {
		var defaults = {
			need: true,
			space :"请输入",
			rule : /^[\S\s]{2,}$/ig,
			error : "格式不正确",
			error_callback : function (el,error) {
				el.e_window({
					html: error
				})
			},
			success_callback : function(el) {
				el.e_window_kill()
			}
		};
		var settings = $.extend({}, defaults, options);
		return this.each(function() {
			var el = $(this);

			var text = settings.space,
				tip_text = $(this).attr("tip_text"),
				text = tip_text ? tip_text : text;
			
			// 默认文字	
			el.addClass('col_gray').val(text);



			// 获得焦点调整样式
			el.focusin(function(event) {
				var el = $(this);
				if (el.val()==text) {
					el.val("").removeClass('col_gray');
				};
			})
			// 失去焦点 调整样式 验证规则 错误提示
			.focusout(function(event) {
				var el = $(this);
				if(el.val()=="" || el.val()==text){
					el.val(text).addClass('col_gray');
				}else {
					// todo 验证规则
				}
			})
			// 输入时调整样式 去除错误提示
			.keyup(function(event) {
				var el = $(this);
				if (el.val()==text) {
					el.addClass('col_gray');
				}else {
					el.removeClass('col_gray');
				};
			});

			// 验证规则
			function rule_validate() {
				if (typeof settings.rule) {};
			}
		});
	}
})(jQuery);