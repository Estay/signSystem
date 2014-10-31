(function($) {
  // 输入提示插件
  $.fn.autoComplete = function(options) {

    if (!this.length) {
      return this;
    }

    var opts = $.extend(true, {}, $.fn.autoComplete.defaults, options);

    this.each(function() {
      var $this = $(this),
        loading = false,
        tmp = $this.val(),
        timeout;
      $this.keyup(function(event) {
        clearTimeout(timeout);
        var text = $this.val();

        // 延迟100毫秒
        setTimeout(function() {
            // 输入不变 退出
            if (tmp == text || text.length < 3) {
              return;
            }

            tmp = text;

            if ($this.val(text) != "") {
              // 计时 
              timeout = setTimeout(function() {


                if (loading) {
                  return;
                };

                loading = true;
                // 获取匹配列表
                $.ajax({
                  url: opts.url,
                  data: {
                    text: tmp
                  }
                })
                .done(function(data) {
                  if (data) {
                    console.log(data);
                    opts.callblack.call($this[0], data);
                  }
                })
                .fail(function() {
                  console.log("获取匹配列表error");
                })
                .always(function() {
                  loading = false;
                });
            }, opts.time);
            }
          }，
          100);

      });

    });

    return this;
  };

  // default options
  $.fn.autoComplete.defaults = {
    time: 300,
    url: "",

    callblack: function(text) {}
  };

})(jQuery);