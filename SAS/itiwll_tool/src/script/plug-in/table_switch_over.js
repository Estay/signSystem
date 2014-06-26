(function($) {
// What does the e_table_switch_over plugin do?
$.fn.e_table_switch_over = function(options) {

  if (!this.length) { return this; }

  var opts = $.extend(true, {}, $.fn.e_table_switch_over.defaults, options);

  this.each(function() {
    var $this = $(this);
    
  });

  return this;
};

// default options
$.fn.e_table_switch_over.defaults = {
  defaultOne: true,
  defaultTwo: false,
  defaultThree: 'yay!'
};

})(jQuery);
