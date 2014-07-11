/*2014年7月11日17:29:26*/
(function($) {
    $.fn.e_input_tip = function(options) {
        var defaults = {
            need: true,
            need_text: "必需输入",
            space: "请输入",
            rule: null,
            error: "格式不正确",
            error_callback: function(error, el) {
                $(this).e_window({
                    top: 5,
                    width: "auto",
                    html: "<div class='red_tip_box'>" + error + "</div>"
                });
            },
            success_callback: function(el) {
                el.e_window_kill();
            },
            init: function() {}
        };
        var settings = $.extend({}, defaults, options);
        return this.each(function() {
            var el = $(this);
            init(el);
            el.focusin(function(event) {
                var el = $(this);
                focusin(el);
            }).focusout(function(event) {
                var el = $(this);
                if (el.val() == "" || el.val() == settings.space) {
                    init(el);
                    if (settings.need) {
                        settings.error_callback.call(this, settings.need_text, el);
                    }
                } else {
                    ruleValidate(el, el.val());
                }
            }).keyup(function(event) {
                var el = $(this);
                if (el.val() == settings.space) {
                    el.addClass("col_gray");
                } else {
                    el.removeClass("col_gray");
                }
            });
            function init(el) {
                var tip_text = $(this).attr("tip_text");
                settings.space = tip_text ? tip_text : settings.space;
                el.addClass("col_gray").val(settings.space);
                settings.init.call(el[0]);
                if (settings.need) {
                    el.attr("rules_error", "");
                } else {
                    el.removeAttr("rules_error");
                }
            }
            function focusin(el) {
                if (el.val() == "" || el.val() == settings.space) {
                    el.val("").removeClass("col_gray");
                }
                settings.success_callback.call(el[0], el);
            }
            function error(el, error) {
                settings.error_callback.call(el[0], settings.error, el);
            }
            function ruleValidate(el, val) {
                if (isRegExp(settings.rule)) {
                    if (!settings.rule.exec(val)) {
                        error(el, settings.error);
                    }
                } else if (settings.rule instanceof Function) {
                    settings.rule.call(el[0], function(error_text, el) {
                        error(el, settings.error);
                    }, el.val());
                } else {
                    return val ? true : false;
                }
            }
            function isRegExp(o) {
                return o && Object.prototype.toString.call(o) === "[object RegExp]";
            }
        });
    };
})(jQuery);

(function($) {
    $.fn.e_img_siz = function(parentClass, mod) {
        parentClass = parentClass ? parentClass : "img_box";
        return this.each(function(index, el) {
            if (this.complete) {
                $.fn.e_img_siz.img_size($(this), parentClass, mod);
            } else {
                $(this).load(function() {
                    $.fn.e_img_siz.img_size($(this), parentClass, mod);
                });
            }
        });
    };
    $.fn.e_img_siz.defaults = {};
    $.fn.e_img_siz.img_size = function(el, parentClass, mod) {
        var parent = el.parents("." + parentClass);
        a = el.height() / el.width() - parent.height() / parent.width();
        if (mod) {
            a = -a;
        }
        if (a < 0) {
            el.height(parent.height());
        } else {
            el.width(parent.width());
        }
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
                    opts.callback(index);
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
    $.fn.e_window = function(options) {
        var defaults = {
            position_mod: "relative",
            relative_mod: "bottom",
            top: 0,
            left: 0,
            width: "400",
            marginTop: 0,
            marginRight: 0,
            box_id: "",
            html: "<p>弹出层 by itiwll@estay</p>"
        }, opt = $.extend(defaults, options);
        return this.each(function() {
            var el = $(this);
            if (el.attr("e_tip_id")) return;
            opt.box_id = opt.box_id ? opt.box_id : "e_box" + new Date().getTime();
            el.attr("e_tip_id", opt.box_id);
            if (opt.position_mod == "center") {
                opt.left = $(window).width() / 2 - opt.width / 2;
            } else if (opt.position_mod == "relative") {
                if (opt.relative_mod == "bottom") {
                    opt.top = el.offset().top + el.height() + opt.top;
                    opt.left = el.offset().left + opt.left;
                } else if (opt.relative_mod == "right") {
                    opt.top = el.offset().top + opt.top;
                    opt.left = el.offset().left + el.width() + opt.left;
                } else if (opt.relative_mod == "top") {
                    opt.top = el.offset().top - opt.top;
                    opt.left = el.offset().left + opt.left;
                } else if (opt.relative_mod == "left") {
                    opt.top = el.offset().top + opt.top;
                    opt.left = el.offset().left + opt.left;
                }
            }
            var win_box = $("<div/>").attr("id", opt.box_id).css({
                position: "absolute",
                left: opt.left,
                top: opt.top,
                marginTop: opt.marginTop,
                marginLight: opt.marginLight,
                width: opt.width
            }).html(opt.html).appendTo("body");
            if (opt.position_mod == "center") win_box.find("img").load(function() {
                win_box.css("top", $(window).height() / 2 - win_box.height() / 2);
            });
            if (opt.position_mod == "relative" && opt.relative_mod == "top") {
                if (win_box.find("img").length) {
                    win_box.find("img").load(function() {
                        win_box.css("top", win_box.offset().top - win_box.height());
                    });
                } else {
                    win_box.css("top", win_box.offset().top - win_box.height());
                }
            }
            if (opt.position_mod == "relative" && opt.relative_mod == "left") {
                if (win_box.find("img").length) {
                    win_box.find("img").load(function() {
                        win_box.css("left", win_box.offset().left - win_box.width());
                    });
                } else {
                    win_box.css("left", win_box.offset().left - win_box.width());
                }
            }
            if ($(window).width() - win_box.offset().left - win_box.width() < 0) {
                win_box.css("left", $(window).width() - win_box.width());
            }
        });
    };
    $.fn.e_window_kill = function() {
        return this.each(function() {
            $("#" + $(this).attr("e_tip_id")).remove();
            $(this).removeAttr("e_tip_id");
        });
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
            if ($element.attr("multiple")) {
                var files = $element.prop("files");
                if (files) {
                    filename = "";
                    for (var i = 0; i < files.length; i++) {
                        filename += files[i].name;
                        filename += "，";
                    }
                    filename = filename.slice(0, -1);
                }
            }
            console.log(filename);
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
    $("#hotel_name").e_input_tip({
        space: "请输入公寓名称",
        rule: function(error_callback, val) {
            var el = $(this);
            if (!val.match(/^[\s\S]{3,}$/)) {
                error_callback("请输入三个以上的字符", el);
            }
            $.ajax({
                url: "/AddHotel/IsOk/",
                dataType: "text",
                data: {
                    text: val
                }
            }).done(function(data) {
                if (data == 0) {
                    error_callback("此公寓已存在", el);
                }
            }).fail(function() {
                alert("服务器验证公寓名称失败");
            });
        }
    });
    $("#hotel_class,#hotel_theme,#hotel_province,#h_city,#h_administrative_region,#h_business_zone").e_input_tip({
        need_text: "必需选择"
    });
    $("#phone_area_code,#hotel_phonehotel_phone").keyup(function(event) {
        $("#phone").val($("#phone_area_code").val() + "-" + $("#hotel_phonehotel_phone").val());
    });
    $("#phone_area_code").e_input_tip({
        space: "区号",
        need_text: "必填",
        error: "错误",
        rule: /^\d{3,4}$/
    });
    $("#hotel_phone").e_input_tip({
        space: "座机号码",
        need_text: "必需填写",
        error: "格式不正确",
        rule: /^\d{7,8}$/
    });
    $("#hotel_fax").e_input_tip({
        space: "传真号码(带区号)",
        need: false,
        error: "格式不正确",
        rule: /^\d{3,4}\-?\d{7,8}$/
    });
    $("#mobeli_phone").e_input_tip({
        space: "11位手机号码",
        need: false,
        error: "格式不正确",
        rule: /^1\d{10}$/
    });
    $("#hotel_address").e_input_tip({
        space: "公寓详细地址",
        error: "格式不正确",
        rule: /^[\s\S]+$/
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
        $("#h_city,#h_administrative_region,#h_business_zone").attr("disabled", "");
        if ($(this).val()) {
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
                $("#h_city").removeAttr("disabled");
            }).fail(function() {
                alert("加载城市数据错误");
            }).always(function() {});
        } else {
            $("#h_city").children().slice(1).remove();
        }
    });
    $("#h_city").change(function(event) {
        $("#h_administrative_region,#h_business_zone").attr("disabled", "");
        if ($(this).val()) {
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
            }).fail(function() {
                alert("加载行政区数据错误");
            }).always(function() {});
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
            }).fail(function() {
                alert("加载商圈数据错误");
            }).always(function() {});
            $("#h_administrative_region,#h_business_zone").removeAttr("disabled");
        } else {
            $("#h_administrative_region,#h_business_zone").each(function(index, el) {
                el.children().slice(1).remove();
            });
        }
    });
    $("#h_administrative_region,#h_business_zone").change(function(event) {
        map.centerAndZoom($(this).find(":selected").text());
    });
    $("#location_box").e_tab_switch({
        callback: function(index) {
            if (index == 0) {
                $("#map_lon,#map_lat").e_window_kill();
            } else {}
        }
    });
    $("#map_lon").keyup(function(event) {
        $("#map_lon_input").val($(this).val());
        $("#map_lon_text").text($(this).val());
    });
    $("#map_lat").keyup(function(event) {
        $("#map_lat_input").val($(this).val());
        $("#map_lat_text").text($(this).val());
    });
    $("#map_lon").e_input_tip({
        space: "输入经度",
        need: false,
        error: "格式不正确",
        rule: /^\d{3}$|^\d{3}.\d+/
    });
    $("#map_lat").e_input_tip({
        space: "输入维度",
        need: false,
        error: "格式不正确",
        rule: /^\d{3}$|^\d{3}.\d+/
    });
    function upload_img(els) {
        els.each(function(index, el) {
            var el = $(el), box = el.parents(".from_path").find(".img_show_box"), info_box = "";
            var html = '<div class="upload_img_box">';
            html += '<div class="img_box"><img class="upload_img" /></div>';
            html += '<p class="img_set">';
            html += '<input type="text" class="upload_img_info">';
            html += '<select class="upload_img_type" name="img_type">';
            html += "</select>";
            html += "</p>";
            html += '<p class="img_del"><a href="">删除</a></p>';
            html += "</div>";
            var upload_tip = '<div class="upload_img_box upload_info_box">';
            upload_tip += '<div class="img_box">';
            upload_tip += "<p style=“margin-top: 20px;”></p>";
            upload_tip += "</div>";
            upload_tip += "</div>";
            el.AjaxFileUpload({
                action: "/help/FileHandle.ashx",
                onSubmit: function(filename) {
                    info_box = $(upload_tip).appendTo(box);
                    info_box.find("p").text(filename + "正在上传中...");
                    return {
                        roomid: this.attr("room_id")
                    };
                },
                onComplete: function(file, response) {
                    for (var i = 0; i < response.length; i++) {
                        var img = response[i];
                        var a = $(html).appendTo(box);
                        a.find("img").attr("src", img.URL).e_img_siz("", true);
                        if (img.PID) {
                            a.data("pid", img.PID);
                            a.find("select").html($("#img_type_sel").html());
                        } else {
                            a.data("pid", "error");
                            a.find(".img_set").addClass("col_red").html(img.Message);
                        }
                    }
                    info_box.remove();
                }
            });
        });
    }
    upload_img($(".upload_img_input"));
    $("#add_img").on("focusout", ".upload_img_info", function(event) {
        var pid = $(this).parents(".upload_img_box").data("pid"), v = $(this).val(), ajax_load = "";
        if (v) {
            ajax_load = $.ajax({
                url: "/ImageProperty/ImageDes",
                type: "GET",
                data: {
                    PID: pid,
                    text: v
                }
            }).done(function(data) {
                if (data == 0) {
                    alert("描述提交失败");
                }
            }).fail(function(data) {
                alert("描述提交错误！错误代码：" + data.status + "," + data.statusText + "。");
            });
        }
    });
    $("#add_img").on("change", ".upload_img_type", function(event) {
        var el = $(this), pid = el.parents(".upload_img_box").data("pid"), v = el.val();
        if (v) {
            $(this).prop("disabled", true);
            $.ajax({
                url: "/ImageProperty/ImageDes",
                type: "GET",
                data: {
                    PID: pid,
                    text: v
                }
            }).done(function(data) {
                if (data == 0) {
                    alert("设置图片类型失败");
                    el[0].selectedIndex = 0;
                }
            }).fail(function(data) {
                alert("提交图片类型错误！错误代码：" + data.status + "," + data.statusText + "。");
            }).always(function() {
                el.removeAttr("disabled");
            });
        }
    });
    $("#add_img").on("click", ".img_del a", function(event) {
        event.preventDefault();
        var box = $(this).parents(".upload_img_box"), pid = box.data("pid"), url = box.find(".upload_img").attr("src"), data = {
            PID: pid == "error" ? 0 : pid,
            text: url
        };
        $.ajax({
            url: "/ImageProperty/ImageDel",
            type: "GET",
            data: data
        }).done(function(data) {
            if (data == 0) {
                alert("删除图片失败");
            } else {
                box.remove();
            }
        }).fail(function(data) {
            alert("删除图片错误！错误代码：" + data.status + "," + data.statusText + "。");
        });
    });
})(jQuery);
//# sourceMappingURL=main.map