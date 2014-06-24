/*2014年6月24日10:52:56*/
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

$(".multiple").change(function(event) {
    var input = $(this).parents(".input_line").prev(".hide"), vals = input.val(), val = $(this).val();
    if (vals.indexOf(val) > -1) {
        vals = vals.replace(RegExp("^" + val + ",|," + val + "|^" + val + "$", "ig"), "");
    } else {
        vals = vals ? vals + "," + val : val;
    }
    input.val(vals);
});
//# sourceMappingURL=main.map