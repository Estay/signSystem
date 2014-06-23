/*2014年6月23日17:38:50*/
(function($) {
    $.fn.e_input_tip = function(options) {
        var defaults = "请输入";
        return this.each(function() {
            var el = $(this);
            var text = options ? options : defaults, attr_data = $(this).attr("tip_text");
            text = attr_data ? attr_data : text;
            el.addClass("col_gray");
            el.val(text);
        });
    };
})(jQuery);

$(".tip_input").e_input_tip();
//# sourceMappingURL=main.map