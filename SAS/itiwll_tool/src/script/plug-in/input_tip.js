/********************************************************
 ** jQuery itiwll_input_tip
 ** 20140623
 **
 **********************************************************/
(function($) {
	$.fn.e_input_tip = function(options) {
		var defaults = {
			need: true,
			need_text: "必需输入",
			space :"请输入",
			rule : null,
			error : "格式不正确",
			error_callback : function (error,el) {
				$(this).e_window({
					top: 5,
					width: "auto",
					html: "<div class='red_tip_box'>"+error+"</div>"
				})
			},
			success_callback : function(el) {
				el.e_window_kill();
			},
			init : function() {}
		};
		var settings = $.extend({}, defaults, options);
		return this.each(function() {
			var el = $(this);

			init(el);



			// 获得焦点
			el.focusin(function(event) {
				var el = $(this);

				// 进入输入状态
				focusin(el);

			})
			// 失去焦点 调整样式 验证规则 
			.focusout(function(event) {
				var el = $(this);

				if(el.val()=="" || el.val()==settings.space){
					//为空回到初始状态
					init(el);
					//必需输入提示
					if(settings.need){
						settings.error_callback.call(this,settings.need_text,el);
					}
				}else {
					// 不为空验证规则
					ruleValidate(el,el.val());
				}
			})
			// 输入时调整样式 去除错误提示
			.keyup(function(event) {
				var el = $(this);
				if (el.val()==settings.space) {
					el.addClass('col_gray');
				}else {
					el.removeClass('col_gray');
				};
			});

			// 初始化的状态
			function init (el) {

				var tip_text = $(this).attr("tip_text");
				settings.space = tip_text ? tip_text : settings.space;
				
				// 默认文字	
				el.addClass('col_gray').val(settings.space);

				settings.init.call(el[0]);

				// 据need标记是否通过规则
				if (settings.need) {
					el.attr('rules_error',"");
				}else{
					el.removeAttr('rules_error');	
				}
			}

			// 输入中的状态
			function focusin(el){
				if (el.val()=="" || el.val()== settings.space) {
					el.val("").removeClass('col_gray');
				};
				settings.success_callback.call(el[0],el);
			}

			// 错误的状态
			function error(el,error) {
				settings.error_callback.call(el[0],error,el);
			}

			// 通过验证的状态
			function success(el) {
				el.removeAttr('rules_error');
				settings.success_callback.call(el[0],el);
			}

			// 验证规则 错误提示
			function ruleValidate(el,val) {
				if (/[\<\>\&]+/.exec(val)) {
					error(el,"不能包含“<”,“>”,“&”等特殊字符");
					return ;
				};

				if (!settings.rule) {
					success(el);
					return ;
				};

				if(isRegExp(settings.rule)){
					if (!settings.rule.exec(val)) {
						// 没通过规则 进入错误状态
						error(el,settings.error);
					}else {
						success(el);
					}
				}else if(settings.rule instanceof Function){
					settings.rule.call(
						el[0],
						function (el) { // 验证通过 回调
							success(el);
						},
						function(error_text,el){ // 验证规则 错误回调
							// 没通过规则 进入错误状态
							error(el,error_text?error_text:settings.error);
						},
						el.val()
					);
				}else {
					return val?true:false;
				}
			}


			// 判断是不是正则对象
			function isRegExp(o){
				return o && Object.prototype.toString.call(o) === '[object RegExp]';
			}
		});
	}
})(jQuery);