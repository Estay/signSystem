(function($) {
	$.fn.e_window = function(options) {
		// 默认设置
		var defaults = {
			position_mod: "relative", //位置模式 居中：center 相对元素 ：relative  相对窗口：absolute
			relative_mod: "bottom", //bottom right top left
			top: 0,
			left: 0,
			width: "400",
			marginTop: 0,
			marginRight: 0,
			layer : false,
			box_id: "",
			html: "<p>弹出层 by itiwll@estay</p>"
		},
			opt = $.extend(defaults, options);

		return this.each(function() {
			var el = $(this);

			if(el.attr('e_tip_id')) return;
			// 层id
			opt.box_id = opt.box_id ? opt.box_id : ("e_box"+ new Date().getTime());
			el.attr('e_tip_id', opt.box_id);
			
			// 层位置
			// 居中模式
			if (opt.position_mod == "center") {
				opt.left = $(window).width() / 2 - opt.width / 2;
			// 相对模式
			} else if (opt.position_mod == "relative") {
				// 底部
				if (opt.relative_mod == "bottom") {
					opt.top = el.offset().top + el.height() + opt.top;
					opt.left = el.offset().left + opt.left;
				// 右边
				} else if (opt.relative_mod == "right") {
					opt.top = el.offset().top + opt.top;
					opt.left = el.offset().left + el.width() + opt.left;
				// 顶边
				} else if (opt.relative_mod == "top") {
					opt.top = el.offset().top - opt.top;
					opt.left = el.offset().left +opt.left;
				// 左边
				} else if (opt.relative_mod == "left") {
					opt.top = el.offset().top + opt.top;
					opt.left = el.offset().left + opt.left;
				}
			}

			var win_box = $("<div/>").attr('id', opt.box_id)
				.css({
					position: "absolute",
					left: opt.left,
					top: opt.top,
					marginTop: opt.marginTop,
					marginLight: opt.marginLight,
					width: opt.width
				}).html(opt.html)
				.appendTo('body');
			if (opt.layer) {
				$('<div style="width: 100%;height: 100%;position: fixed;background: #000;"></div>')
				.appendTo('body');
			};
			
			// 有高宽后 调整层位置
			if (opt.position_mod == "center")
				win_box.find('img').load(function() {
					win_box.css("top", $(window).height() / 2 - win_box.height() / 2);
				});
			if (opt.position_mod == "relative" && opt.relative_mod == "top"){
				// top
				if(win_box.find('img').length){
					win_box.find('img').load(function() {
						win_box.css("top", win_box.offset().top-win_box.height());
					});
				}else {
					win_box.css("top", win_box.offset().top-win_box.height());
				}
				// left

			}
			if (opt.position_mod == "relative" && opt.relative_mod == "left") {
				if(win_box.find('img').length){
					win_box.find('img').load(function() {
						win_box.css("left", win_box.offset().left-win_box.width())
					});
				}else {
					win_box.css("left", win_box.offset().left-win_box.width());
				}
			}
			if($(window).width()-win_box.offset().left-win_box.width()<0){
				win_box.css('left', $(window).width()-win_box.width());
			}
		});
	}
	$.fn.e_window_kill = function (not_kill_list) {
		var all = all?true:false;
		return this.each(function() {
			if(not_kill_list){
				$("#"+$(this).attr('e_tip_id')).remove();
				$(this).removeAttr('e_tip_id');
			}else {
				$("#"+$(this).attr('e_tip_id')).not("[id*='list']").remove();
				$(this).removeAttr('e_tip_id');
			}
		});
	}
})(jQuery);