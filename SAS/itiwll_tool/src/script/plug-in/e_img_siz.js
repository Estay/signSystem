/********************************************************
** jQuery e_img_siz
**
**
**********************************************************/
(function($){
	$.fn.e_img_siz = function(parentClass,mod) {
		parentClass = parentClass ? parentClass : "img_box";
		return this.each(function(index,el) {
			if(this.complete){
				$.fn.e_img_siz.img_size($(this),parentClass,mod);
			}else{
				$(this).load(function() {
				$.fn.e_img_siz.img_size($(this),parentClass,mod);
				});
			}
		});
	};

	// 默认设置2
	$.fn.e_img_siz.defaults = {

	};

	$.fn.e_img_siz.img_size = function (el,parentClass,mod) {
		var parent = el.parents("."+parentClass)
			a = el.height()/el.width()-parent.height()/parent.width();

		if(mod){
			a=-a;
		}
		if(a<0){
			el.height(parent.height());
		}else {
			el.width(parent.width());
		}
	}

})(jQuery);