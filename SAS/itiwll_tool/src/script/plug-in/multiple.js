(function($) {
	// 多选插件
	$.fn.e_multiple = function(options) {

		if (!this.length) {
			return this;
		}

		var opts = $.extend(true, {}, $.fn.e_multiple.defaults, options);

		this.each(function() {
			var $this = $(this),
				multiple_inputs = $this.find('.' + opts.checkbox_class),
				input = $("#" + opt.input_id);
			multiple_inputs.change(function(event) {
					vals = input.val(),
					val = $(this).val();
				if (vals.indexOf(val) > -1) {
					vals = vals.replace(RegExp("^" + val + ",|," + val + "|^" + val + "$", "ig"), '');
				} else {
					vals = vals ? vals + "," + val : val;
				};
				input.val(vals);
			});

		});

		return this;
	};

	// default options
	$.fn.e_multiple.defaults = {
		input_id: "",
		checkbox_class: "multiple"
	};

})(jQuery);