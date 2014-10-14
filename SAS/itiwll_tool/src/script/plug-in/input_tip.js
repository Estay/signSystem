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
			check : false, //失去焦点是否验证
			submit_check : true, //表单提交是否验证
			error : "格式不正确",
			error_callback : function (error,el) {
				$(this).e_window({
					top: 5,
					width: "auto",
					html: "<div class='red_tip_box'>"+error+"</div>"
				})
			},
			space_callback : function(need_text,el) {
				$(this).e_window({
					top: 5,
					width: "auto",
					html: "<div class='red_tip_box'>"+need_text+"</div>"
				})
			},
			success_callback : function(el) {
				el.e_window_kill();
			},
			init : function() {}
		};
		var settings = $.extend({}, defaults, options);
		return this.each(function() {
			var el = $(this),
				form = el .parents("form");

			if (!el.val()) {
				init(el);
			};



			// 获得焦点
			el.focusin(function(event) {
				var el = $(this);

				// 进入输入状态
				focusin(el);

			})
			// 失去焦点 调整样式
			.focusout(function(event) {

				if(el.val()=="" || el.val()==settings.space){
					//为空回到初始状态
					init(el);
				}
				if (settings.check) {	
					ruleValidate(el,el.val());
				};
			})
			// 输入时调整样式 去除错误提示
			.keyup(function(event) {
				var el = $(this);
				if (el.val()==settings.space) {
					el.addClass('col_gray');
				}else {
					el.removeClass('col_gray');
				};
			})
			// 绑定触发验证
			.bind("input_tip_checking",function () {
				var el = $(this);

				// 验证规则
				ruleValidate(el,el.val());
			});

				
			// 表单提交时验证
			form.submit(function(event) {
				if (el.attr("not_validate")) return;
				var val = el.val();
				if (settings.submit_check) {
					if (settings.check) {
						// 失去焦点是否已验通过
						if (el.attr('rules_error')) {
							event.preventDefault();
							event.stopImmediatePropagation();
							ruleValidate(el, val);
							$("html,body").animate({scrollTop: el.offset().top}, 800);
						}else {
							if(val=="" || val == settings.space){
								el.val("");
							}
						}
					}else{
						// 验证规则是否通过
						if (!ruleValidate(el, val)) {
							event.preventDefault();
							event.stopImmediatePropagation();
							console.log("没通过规则");
							console.log(el);
							$("html,body").animate({scrollTop: el.offset().top}, 800);
						} else {
							if(val=="" || val == settings.space){
								el.val("");
							}
						}
					}


				}
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
					el.attr('rules_error',"true");
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
					return false;
				};


				// 没有规则
				if (!settings.rule) {

					// 值为空或默认
					if(val=="" || val == settings.space){
						//为空回到初始状态
						init(el);
						//必需输入提示
						if(settings.need){
							settings.space_callback.call(el[0],settings.need_text,el);
							return false;
						}
					}

					success(el);
					return true;
				};


				// 规则为正则时
				if(isRegExp(settings.rule)){

					// 值为空或默认
					if(val=="" || val == settings.space){
						//为空回到初始状态
						init(el);
						//必需输入提示
						if(settings.need){
							settings.space_callback.call(el[0],settings.need_text,el);
							return false;
						}else {
							success(el);
							return true;
						}
					}

					if (!settings.rule.exec(val)) {
						// 没通过规则 进入错误状态
						error(el,settings.error);
						return false;
					}else {
						success(el);
						return true;
					}

				// 规则为Function时
				}else if(settings.rule instanceof Function){

					// 值为空或默认
					if(val=="" || val == settings.space){
						//为空回到初始状态
						init(el);
						//必需输入提示
						if(settings.need){
							settings.space_callback.call(el[0],settings.need_text,el);
							return false;
						}
					}

					return settings.rule.call(
						el[0],
						function (cb_el) { // 验证通过 回调
							success(cb_el?cb_el:el);
						},
						function(error_text,cb_el){ // 验证规则 错误回调
							// 没通过规则 进入错误状态
							error(cb_el?cb_el:el,error_text?error_text:settings.error);
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