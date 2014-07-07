/*2014年7月7日13:47:32*/
(function($) {
    $.fn.e_input_tip = function(options) {
        var defaults = "请输入";
        return this.each(function() {
            var el = $(this);
            var text = options ? options : defaults, tip_text = $(this).attr("tip_text"), text = tip_text ? tip_text : text;
            el.addClass("col_gray");
            el.val(text);
            el.focusin(function(event) {
                if (el.val() == text) {
                    el.val("").removeClass("col_gray");
                }
            }).focusout(function(event) {
                if (el.val() == "") {
                    el.val(text).addClass("col_gray");
                }
            }).keyup(function(event) {
                if (el.val() == text) {
                    el.addClass("col_gray");
                } else {
                    el.removeClass("col_gray");
                }
            });
        });
    };
})(jQuery);

(function($) {
    $.fn.e_tab_switch = function(options) {
        if (!this.length) {
            return this;
        }
        var opts = $.extend(true, {}, $.fn.e_tab_switch.defaults, options);
        this.each(function() {
            var $this = $(this), el_tabs = $this.find("." + opts.tab_class), el_boxs = $this.find("." + opts.box_class);
            el_tabs.click(function(event) {
                event.preventDefault();
                var index = el_tabs.index($(this));
                $(this).addClass("set");
                el_tabs.not($(this)).removeClass("set");
                el_boxs.hide().eq(index).show();
                if (opts.callback) {
                    opts.callback();
                }
            });
        });
        return this;
    };
    $.fn.e_tab_switch.defaults = {
        tab_class: "e_tab",
        box_class: "e_tab_box",
        callback: null
    };
})(jQuery);

(function($) {
    $.fn.AjaxFileUpload = function(options) {
        var defaults = {
            action: "upload.php",
            onChange: function(filename) {},
            onSubmit: function(filename) {},
            onComplete: function(filename, response) {}
        }, settings = $.extend({}, defaults, options), randomId = function() {
            var id = 0;
            return function() {
                return "_AjaxFileUpload" + id++;
            };
        }();
        return this.each(function() {
            var $this = $(this);
            if ($this.is("input") && $this.attr("type") === "file") {
                $this.bind("change", onChange);
            }
        });
        function onChange(e) {
            var $element = $(e.target), id = $element.attr("id"), $clone = $element.removeAttr("id").clone().attr("id", id).AjaxFileUpload(options), filename = $element.val().replace(/.*(\/|\\)/, ""), iframe = createIframe(), form = createForm(iframe);
            $clone.insertBefore($element);
            settings.onChange.call($clone[0], filename);
            iframe.bind("load", {
                element: $clone,
                form: form,
                filename: filename
            }, onComplete);
            form.append($element).bind("submit", {
                element: $clone,
                iframe: iframe,
                filename: filename
            }, onSubmit).submit();
        }
        function onSubmit(e) {
            var data = settings.onSubmit.call(e.data.element, e.data.filename);
            if (data === false) {
                $(e.target).remove();
                e.data.iframe.remove();
                return false;
            } else {
                for (var variable in data) {
                    $("<input />").attr("type", "hidden").attr("name", variable).val(data[variable]).appendTo(e.target);
                }
            }
        }
        function onComplete(e) {
            var $iframe = $(e.target), doc = ($iframe[0].contentWindow || $iframe[0].contentDocument).document, response = doc.body.innerHTML;
            if (response) {
                response = $.parseJSON(response);
            } else {
                response = {};
            }
            settings.onComplete.call(e.data.element, e.data.filename, response);
            e.data.form.remove();
            $iframe.remove();
        }
        function createIframe() {
            var id = randomId();
            $("body").append('<iframe src="javascript:false;" name="' + id + '" id="' + id + '" style="display: none;"></iframe>');
            return $("#" + id);
        }
        function createForm(iframe) {
            return $("<form />").attr({
                method: "post",
                action: settings.action,
                enctype: "multipart/form-data",
                target: iframe[0].name
            }).hide().appendTo("body");
        }
    };
})(jQuery);

(function($) {
    var estay_sas = {};
    $(".tip_input").e_input_tip();
    $("#phone_area_code,#hotel_phonehotel_phone").keyup(function(event) {
        $("#phone").val($("#phone_area_code").val() + "-" + $("#hotel_phonehotel_phone").val());
    });
    $(".multiple").change(function(event) {
        var input = $(this).parents(".input_line").prev(".hide");
        input = input.length ? input : $(this).parents(".input_line").find(".hide");
        var vals = input.val(), val = $(this).val();
        if (vals.indexOf(val) > -1) {
            vals = vals.replace(RegExp("^" + val + ",|," + val + "|^" + val + "$", "ig"), "");
        } else {
            vals = vals ? vals + "," + val : val;
        }
        input.val(vals);
    });
    $("#hotel_province").change(function(event) {
        map.centerAndZoom($(this).find(":selected").text());
        $.ajax({
            url: "/help/location.ashx",
            type: "GET",
            dataType: "json",
            data: {
                type: "city",
                value: $(this).val()
            }
        }).done(function(data) {
            $("#h_city").children().slice(1).remove();
            $("#h_administrative_region").children().slice(1).remove();
            $("#h_business_zone").children().slice(1).remove();
            for (var i = 0; i < data.length; i++) {
                var city = data[i];
                var option = '<option value="' + city.id + '">' + city.name + "</option>";
                $("#h_city").append(option);
            }
        }).fail(function() {}).always(function() {});
    });
    $("#h_city").change(function(event) {
        map.centerAndZoom($(this).find(":selected").text());
        $.ajax({
            url: "/help/location.ashx",
            type: "GET",
            dataType: "json",
            data: {
                type: "region",
                value: $(this).val()
            }
        }).done(function(data) {
            $("#h_administrative_region").children().slice(1).remove();
            for (var i = 0; i < data.length; i++) {
                var city = data[i];
                var option = '<option value="' + city.id + '">' + city.name + "</option>";
                $("#h_administrative_region").append(option);
            }
        }).fail(function() {}).always(function() {});
        $.ajax({
            url: "/help/location.ashx",
            type: "GET",
            dataType: "json",
            data: {
                type: "commercial",
                value: $(this).val()
            }
        }).done(function(data) {
            $("#h_business_zone").children().slice(1).remove();
            for (var i = 0; i < data.length; i++) {
                var city = data[i];
                var option = '<option value="' + city.id + '">' + city.name + "</option>";
                $("#h_business_zone").append(option);
            }
        }).fail(function() {}).always(function() {});
    });
    $("#h_administrative_region,#h_business_zone").change(function(event) {
        map.centerAndZoom($(this).find(":selected").text());
    });
    $("#location_box").e_tab_switch();
    $("#map_lon").keyup(function(event) {
        $("#map_lon_input").val($(this).val());
        $("#map_lon_text").text($(this).val());
    });
    $("#map_lat").keyup(function(event) {
        $("#map_lat_input").val($(this).val());
        $("#map_lat_text").text($(this).val());
    });
    $(".upload_img_box").on("click", ".upload_img_btn", function(event) {
        event.preventDefault();
        $(this).siblings(".upload_img_input").click();
    });
    $(".upload_img_input").AjaxFileUpload({
        action: "/help/FileHandle.ashx",
        onSubmit: function(filename) {
            console.log($(this));
            console.log(filename);
            $(this).siblings(".upload_img_btn").text("上传中...");
        },
        onComplete: function(filename, response) {
            $(".upload_img_box").append($("<img />").attr("src", filename).attr("width", 200));
        }
    });
})(jQuery);
//# sourceMappingURL=main.map