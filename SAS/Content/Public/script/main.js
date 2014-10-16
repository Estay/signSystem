/*2014年10月16日11:06:45*/
(function($) {
    $.fn.e_input_tip = function(options) {
        var defaults = {
            need: true,
            need_text: "必需输入",
            space: "请输入",
            rule: null,
            check: false,
            submit_check: true,
            error: "格式不正确",
            error_callback: function(error, el) {
                $(this).e_window({
                    top: 5,
                    width: "auto",
                    html: "<div class='red_tip_box'>" + error + "</div>"
                });
            },
            space_callback: function(need_text, el) {
                $(this).e_window({
                    top: 5,
                    width: "auto",
                    html: "<div class='red_tip_box'>" + need_text + "</div>"
                });
            },
            success_callback: function(el) {
                el.e_window_kill();
            },
            init: function() {}
        };
        var settings = $.extend({}, defaults, options);
        return this.each(function() {
            var el = $(this), form = el.parents("form");
            if (!el.val()) {
                init(el);
            }
            el.focusin(function(event) {
                var el = $(this);
                focusin(el);
            }).focusout(function(event) {
                if (el.val() == "" || el.val() == settings.space) {
                    init(el);
                }
                if (settings.check) {
                    ruleValidate(el, el.val());
                }
            }).keyup(function(event) {
                var el = $(this);
                if (el.val() == settings.space) {
                    el.addClass("col_gray");
                } else {
                    el.removeClass("col_gray");
                }
            }).bind("input_tip_checking", function() {
                var el = $(this);
                ruleValidate(el, el.val());
            });
            form.submit(function(event) {
                if (el.attr("not_validate")) return;
                var val = el.val();
                if (settings.submit_check) {
                    if (settings.check) {
                        if (el.attr("rules_error")) {
                            event.preventDefault();
                            event.stopImmediatePropagation();
                            ruleValidate(el, val);
                            $("html,body").animate({
                                scrollTop: el.offset().top
                            }, 800);
                        } else {
                            if (val == "" || val == settings.space) {
                                el.val("");
                            }
                        }
                    } else {
                        if (!ruleValidate(el, val)) {
                            event.preventDefault();
                            event.stopImmediatePropagation();
                            console.log("没通过规则");
                            console.log(el);
                            $("html,body").animate({
                                scrollTop: el.offset().top
                            }, 800);
                        } else {
                            if (val == "" || val == settings.space) {
                                el.val("");
                            }
                        }
                    }
                }
            });
            function init(el) {
                var tip_text = $(this).attr("tip_text");
                settings.space = tip_text ? tip_text : settings.space;
                el.addClass("col_gray").val(settings.space);
                settings.init.call(el[0]);
                if (settings.need) {
                    el.attr("rules_error", "true");
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
                settings.error_callback.call(el[0], error, el);
            }
            function success(el) {
                el.removeAttr("rules_error");
                settings.success_callback.call(el[0], el);
            }
            function ruleValidate(el, val) {
                if (/[\<\>\&]+/.exec(val)) {
                    error(el, "不能包含“<”,“>”,“&”等特殊字符");
                    return false;
                }
                if (!settings.rule) {
                    if (val == "" || val == settings.space) {
                        init(el);
                        if (settings.need) {
                            settings.space_callback.call(el[0], settings.need_text, el);
                            return false;
                        }
                    }
                    success(el);
                    return true;
                }
                if (isRegExp(settings.rule)) {
                    if (val == "" || val == settings.space) {
                        init(el);
                        if (settings.need) {
                            settings.space_callback.call(el[0], settings.need_text, el);
                            return false;
                        } else {
                            success(el);
                            return true;
                        }
                    }
                    if (!settings.rule.exec(val)) {
                        error(el, settings.error);
                        return false;
                    } else {
                        success(el);
                        return true;
                    }
                } else if (settings.rule instanceof Function) {
                    if (val == "" || val == settings.space) {
                        init(el);
                        if (settings.need) {
                            settings.space_callback.call(el[0], settings.need_text, el);
                            return false;
                        }
                    }
                    return settings.rule.call(el[0], function(cb_el) {
                        success(cb_el ? cb_el : el);
                    }, function(error_text, cb_el) {
                        error(cb_el ? cb_el : el, error_text ? error_text : settings.error);
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
            onComplete: function(filename, response) {},
            onError: function(filename) {},
            outTime: 12e4
        }, settings = $.extend({}, defaults, options), randomId = function() {
            var id = 0;
            return function() {
                return "_AjaxFileUpload" + new Date().getTime();
            };
        }(), response = "";
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
            $clone.insertBefore($element);
            settings.onChange.call($clone[0], filename);
            iframe.bind("load", {
                element: $clone,
                form: form,
                filename: filename
            }, onComplete);
            try {
                form.append($element).bind("submit", {
                    element: $clone,
                    iframe: iframe,
                    filename: filename
                }, onSubmit).submit();
            } catch (err) {
                console.log(err);
            }
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
                setTimeout(function() {
                    if (!response) settings.onError.call(e.data.element, e.data.filename);
                }, settings.outTime);
            }
        }
        function onComplete(e) {
            var $iframe = $(e.target);
            if ($iframe[0].contentWindow || $iframe[0].contentDocument) {
                var doc = ($iframe[0].contentWindow || $iframe[0].contentDocument).document;
                response = doc.body.innerHTML;
                if (response) {
                    response = $.parseJSON(response);
                } else {
                    response = {};
                }
                settings.onComplete.call(e.data.element, e.data.filename, response);
                e.data.form.remove();
                $iframe.remove();
            } else {
                settings.onError.call(e.data.element, e.data.filename);
            }
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

(function() {})();

(function($) {
    if (!window.console) window.console = {};
    var console = window.console;
    var funcs = [ "assert", "clear", "count", "debug", "dir", "dirxml", "error", "exception", "group", "groupCollapsed", "groupEnd", "info", "log", "markTimeline", "profile", "profileEnd", "table", "time", "timeEnd", "timeStamp", "trace", "warn" ];
    for (var i = 0, l = funcs.length; i < l; i++) {
        var func = funcs[i];
        if (!console[func]) console[func] = function() {};
    }
    if (!console.memory) console.memory = {};
    $("#login_id_input").e_input_tip({
        space: "用户名",
        need: true
    });
    $("#login_password_input_show").click(function(event) {
        $(this).hide();
        $(this).prev().show().focus();
    });
    $("#login_password_input").e_input_tip({
        space: "",
        need: true,
        space_callback: function(need_text, el) {
            el.hide().next().show().e_window({
                top: 5,
                width: "auto",
                html: "<div class='red_tip_box'>" + need_text + "</div>"
            });
        },
        success_callback: function(el) {
            el.next().e_window_kill();
        }
    });
    $("#login_code_input").e_input_tip({
        space: "验证码",
        need: true
    });
    $("#login_btn").click(function(event) {
        event.preventDefault();
        $(this).parents("form").submit();
    });
    $(".del_hotel").click(function(event) {
        var el = $(this);
        var r = confirm("确认删除公寓<" + el.attr("hotel_name") + ">?！");
        event.preventDefault();
        if (r == true) {
            $.ajax({
                url: el[0].href,
                type: "GET",
                data: {
                    hotel_id: el.attr("hotel_id")
                }
            }).done(function(date) {
                console.log("success");
                if (date == 1) {
                    location.reload();
                } else {
                    alert("删除失败");
                }
            }).fail(function() {
                console.log("error");
            }).always(function() {
                console.log("complete");
            });
        }
    });
    $("#hotel_name").e_input_tip({
        space: "请输入公寓名称",
        check: true,
        rule: function(success_callback, error_callback, val) {
            var el = $(this);
            if (!val.match(/^[\s\S]{3,}$/)) {
                error_callback("请输入三个以上的字符", el);
                return;
            }
            $.ajax({
                url: "/Common/IsOkHotel",
                dataType: "text",
                data: {
                    text: val
                }
            }).done(function(data) {
                if (data == 0) {
                    error_callback("此公寓已存在", el);
                } else {
                    success_callback(el);
                }
            }).fail(function() {
                alert("服务器验证公寓名称失败");
            });
        }
    });
    $("#hotel_class,#hotel_theme,#hotel_province,#h_city,#h_administrative_region,#h_business_zone").e_input_tip({
        need_text: "必需选择"
    });
    $("#phone_area_code,#hotel_phone").focusout(function(event) {
        console.log("电话值处理");
        $("#phone").val($("#phone_area_code").val() + "-" + $("#hotel_phone").val());
    });
    (function($) {
        var area = $("#phone_area_code"), fixed_phone = $("#hotel_phone"), area_space = "区号", fixed_phone_space = "座机号码";
        area.e_input_tip({
            need: false,
            space: area_space,
            need_text: "请填写区号",
            error: "格式错误",
            rule: function(success_callback, error_callback, val) {
                if (val == "" || val == area_space) {
                    if (fixed_phone.val() != "" && fixed_phone.val() != fixed_phone_space) {
                        error_callback("请填写区号");
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    if (/^\d{3,4}$/.exec(val)) {
                        success_callback();
                        return true;
                    } else {
                        error_callback();
                        return false;
                    }
                }
            }
        });
        fixed_phone.e_input_tip({
            need: false,
            space: fixed_phone_space,
            need_text: "请填写座机号码",
            error: "格式错误",
            rule: function(success_callback, error_callback, val) {
                if (val == "" || val == fixed_phone_space) {
                    if (area.val() != "" && area.val() != area_space) {
                        error_callback("请填写座机号码");
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    if (/^\d{7,8}$/.exec(val)) {
                        success_callback();
                        return true;
                    } else {
                        error_callback();
                        return false;
                    }
                }
            }
        });
    })($);
    $("#hotel_fax").e_input_tip({
        space: "(如:0755-83386677)",
        need: false,
        error: "格式不正确",
        rule: /^\d{3,4}\-?\d{7,8}$/
    });
    $("#mobeli_phone").e_input_tip({
        space: "11位手机号码",
        need_text: "必填,接收预订信息",
        error: "格式不正确",
        rule: /^1\d{10}$/
    });
    $("#hotel_address").e_input_tip({
        space: "公寓详细地址",
        error: "格式不正确",
        rule: /^[\s\S]+$/
    });
    (function($) {
        var lon = $("#map_lon_input").val(), lat = $("#map_lat_input").val();
        if (lon && lat) {
            mapClick(lon, lat);
        }
        var province = $("#hotel_province"), city = $("#h_city"), region = $("#h_administrative_region"), zone = $("#h_business_zone");
        if (province.length) {
            province.change(function(event) {
                seted_province();
            });
            function seted_province() {
                map.centerAndZoom(province.find(":selected").text());
                city.add(region).add(zone).attr("disabled", "");
                if (province.val()) {
                    $.ajax({
                        url: "/help/location.ashx",
                        type: "GET",
                        dataType: "json",
                        data: {
                            type: "city",
                            value: province.val()
                        }
                    }).done(function(data) {
                        city.children().slice(1).remove();
                        region.children().slice(1).remove();
                        zone.children().slice(1).remove();
                        var val = city.attr("original"), option = "";
                        for (var i = 0; i < data.length; i++) {
                            var city_data = data[i];
                            option = option + '<option value="' + city_data.id + '"' + (val == city_data.id ? " selected" : "") + ">" + city_data.name + "</option>";
                        }
                        city.append(option);
                        if (val) {
                            city.removeClass("col_gray");
                            region.removeClass("col_gray");
                            zone.removeClass("col_gray");
                            seted_city();
                        }
                        city.removeAttr("disabled");
                    }).fail(function() {
                        alert("加载城市数据错误");
                    }).always(function() {});
                } else {
                    city.children().slice(1).remove();
                }
            }
            seted_province();
            city.change(function(event) {
                seted_city();
            });
            function seted_city() {
                region.add(zone).attr("disabled", "");
                if (city.val()) {
                    map.centerAndZoom(city.find(":selected").text());
                    $.ajax({
                        url: "/help/location.ashx",
                        type: "GET",
                        dataType: "json",
                        data: {
                            type: "region",
                            value: city.val()
                        }
                    }).done(function(data) {
                        region.children().slice(1).remove();
                        var val = region.attr("original"), option = "";
                        for (var i = 0; i < data.length; i++) {
                            var city_data = data[i];
                            option = option + '<option value="' + city_data.id + '"' + (val == city_data.id ? " selected" : "") + ">" + city_data.name + "</option>";
                        }
                        region.append(option);
                    }).fail(function() {
                        alert("加载行政区数据错误");
                    }).always(function() {});
                    $.ajax({
                        url: "/help/location.ashx",
                        type: "GET",
                        dataType: "json",
                        data: {
                            type: "commercial",
                            value: city.val()
                        }
                    }).done(function(data) {
                        zone.children().slice(1).remove();
                        var val = zone.attr("original"), option = "";
                        for (var i = 0; i < data.length; i++) {
                            var city_data = data[i];
                            option = option + '<option value="' + city_data.id + '"' + (val == city_data.id ? " selected" : "") + ">" + city_data.name + "</option>";
                        }
                        zone.append(option);
                    }).fail(function() {
                        alert("加载商圈数据错误");
                    }).always(function() {});
                    region.add(zone).removeAttr("disabled");
                } else {
                    console.log("message");
                }
            }
            region.add(zone).change(function(event) {
                map.centerAndZoom(city.find(":selected").text());
            });
        }
    })($);
    (function() {
        var lon = $("#map_lon"), lat = $("#map_lat"), location_box = $("#location_box");
        lon.keyup(function(event) {
            $("#map_lon_input").val($(this).val());
            $("#map_lon_text").text($(this).val());
        });
        lat.keyup(function(event) {
            $("#map_lat_input").val($(this).val());
            $("#map_lat_text").text($(this).val());
        });
        location_box.e_tab_switch({
            callback: function(index) {
                if (index == 0) {
                    $("#show_coordinates").show();
                    lat.add(lon).e_window_kill().unbind("input_tip_checking").attr("not_validate", "true");
                } else {
                    if (!lon.attr("not_validate")) {
                        lon.e_input_tip({
                            space: "输入经度",
                            error: "格式不正确",
                            rule: /^\-{0,1}\d{1,3}$|^\-{0,1}\d{1,3}.\d+$/
                        });
                        lat.e_input_tip({
                            space: "输入维度",
                            error: "格式不正确",
                            rule: /^\-{0,1}\d{1,3}$|^\-{0,1}\d{1,3}.\d+$/
                        });
                    } else {
                        lat.add(lon).removeAttr("not_validate");
                    }
                    $("#show_coordinates").hide();
                }
            }
        });
        $("#map_lon_input").e_input_tip({
            space: "",
            space_callback: function() {
                alert("请正确的设置地图坐标");
            }
        });
    })();
    $("#hotel_building,#hotel_room_count").e_input_tip({
        space: "0",
        error: "格式不正确",
        rule: /^\d+$/
    });
    (function() {
        var open_year = $(".select_yeae").eq(0), decoration_year = $(".select_yeae").eq(1), open_month = $(".select_month").eq(0), decoration_month = $(".select_month").eq(1);
        open_year_o = open_year.find("option"), decoration_year_o = decoration_year.find("option"), 
        open_month_o = open_month.find("option"), decoration_month_o = decoration_month.find("option"), 
        clone_y = open_year.clone(false, false), clone_m = open_month.clone(false, false);
        open_year.add(open_month).change(function(event) {
            if (decoration_year[0].selectedIndex != 0 && open_year[0].selectedIndex >= decoration_year[0].selectedIndex) {
                decoration_year[0].selectedIndex = open_year[0].selectedIndex;
                if (open_month[0].selectedIndex != 0 && decoration_month[0].selectedIndex != 0 && open_month[0].selectedIndex < decoration_month[0].selectedIndex) {
                    decoration_month[0].selectedIndex = open_month[0].selectedIndex;
                }
            }
        });
        decoration_month.add(decoration_year).change(function(event) {
            var a = decoration_year[0].selectedIndex * decoration_month[0].selectedIndex * open_year[0].selectedIndex * open_month[0].selectedIndex, b = decoration_month[0].selectedIndex - decoration_year[0].selectedIndex * 12 + open_year[0].selectedIndex * 12 - open_month[0].selectedIndex;
            if (a > 0 && b > 0) {
                alert("装修时间不能在开业时间之后");
                decoration_year[0].selectedIndex = decoration_month[0].selectedIndex = 0;
            }
        });
    })();
    var time_select = $("#hotel_built_year,#hotel_decoration_time_year").siblings("select");
    time_select.change(function(event) {
        var p = $(this).parent(), val = p.find(".select_yeae").val() + p.find(".select_month").val();
        p.find("input.hide").val(val);
    });
    time_select.siblings("select").e_input_tip({
        need_text: "必需选择"
    });
    $("#hotel_specialty").e_input_tip({
        space: "请输入公寓的特色",
        need: false,
        error: "最大可输入2000个字符",
        rule: /^[\S\s]{0,2000}$/,
        error_callback: function(error, el) {
            $(this).e_window({
                relative_mod: "right",
                left: 10,
                width: "auto",
                html: "<div class='red_tip_box'>" + error + "</div>"
            });
        }
    });
    $("#hotel_abstract").e_input_tip({
        space: "请输入公寓简介(2000字以内)",
        error: "最大可输入2000个字符",
        rule: /^[\S\s]{0,2000}$/,
        error_callback: function(error, el) {
            $(this).e_window({
                relative_mod: "right",
                left: 30,
                width: "auto",
                html: "<div class='red_tip_box'>" + error + "</div>"
            });
        }
    });
    $("#hotel_place").e_input_tip({
        space: "请输入交通位置(2000字以内)",
        error: "最大可输入2000个字符",
        rule: /^[\S\s]{0,2000}$/,
        error_callback: function(error, el) {
            $(this).e_window({
                relative_mod: "right",
                left: 30,
                width: "auto",
                html: "<div class='red_tip_box'>" + error + "</div>"
            });
        }
    });
    (function($) {
        function set_val(input) {
            var data_arr = input.val().split(","), multiple = input.next().find(".multiple");
            for (var i = 0; i < data_arr.length; i++) {
                var val = data_arr[i];
                multiple.filter("[value=" + val + "]").attr("checked", "true");
            }
        }
        function set_val_b(input) {
            var data_arr = input.val().split("、"), label = input.next().find(".multiple").next();
            for (var i = 0; i < data_arr.length; i++) {
                var val = data_arr[i];
                label.filter(function() {
                    return $(this).text() == val;
                }).prev().attr("checked", "true");
            }
        }
        var f_input = $("#facilities_hide"), s_input = $("#generalAmenities_hide");
        if (f_input.length) {
            set_val(f_input);
            set_val(s_input);
            set_val($(".multiple_value"));
        }
    })($);
    $("#room_name").e_input_tip({
        space: "（如：高级大床房）",
        check: true,
        rule: function(success_callback, error_callback, val) {
            var el = $(this);
            if (!val.match(/^[\s\S]{3,}$/)) {
                error_callback("请输入三个以上的字符", el);
                return;
            }
            $.ajax({
                url: "/Common/isOkRoom",
                dataType: "text",
                data: {
                    text: val,
                    hotelId: $("[name=hotel_id]").val()
                }
            }).done(function(data) {
                if (data == 0) {
                    error_callback("此房型已存在", el);
                } else {
                    success_callback(el);
                }
            }).fail(function() {
                alert("服务器验证公寓名称失败");
            });
        }
    });
    $("#room_count").e_input_tip({
        space: 0,
        rule: /^\d+$/
    });
    $("#default_price").e_input_tip({
        space: "0",
        need_text: "必须选择",
        rule: /^\d+$|^\d+.\d+$/
    });
    $("#people_number").e_input_tip({
        need_text: "必须选择宜住人数"
    });
    $("#room_area").e_input_tip({
        space: 0,
        rule: /^\d+$/
    });
    $("#room_floor").e_input_tip({
        space: "",
        rule: /^[\s\S]+$/
    });
    (function() {
        $(".bed_add").click(function(event) {
            event.preventDefault();
            var clone = $(".bed_item.hide").clone().removeClass("hide");
            $(this).before(clone);
            clone.find(".bed,.number").e_input_tip({
                check: true,
                need_text: "必须选择床型"
            });
        });
        var bed_input = $("#bed_input"), bed_val = bed_input.val(), bed_items = $(".bed_item");
        if (bed_val) {
            var bed_arr = bed_val.split(",");
            bed_items.eq(1).remove();
            for (var i = 0; i < bed_arr.length; i++) {
                if (bed_arr[i]) {
                    var bed = bed_arr[i].split("|");
                    var clone = $(".bed_item.hide").clone().removeClass("hide");
                    clone.find(".bed").val(bed[0]);
                    clone.find(".number").val(bed[1]);
                    if (i == 0) {
                        clone.find(".bed_del").remove();
                    }
                    $(".bed_add").before(clone);
                }
            }
            bed_items = $(".bed_item");
        }
        bed_items.not(".hide").find(".bed,.number").e_input_tip({
            need_text: "必须选择"
        });
        $("#bed_box").on("click", ".bed_del", function(event) {
            event.preventDefault();
            var box = $(this).parents(".bed_item");
            box.find(".bed,.number").e_window_kill().unbind("input_tip_checking").attr("not_validate", "true");
            box.remove();
            setBedInput();
        });
        $("#bed_box").on("change", "select", function(event) {
            setBedInput();
        });
        function setBedInput() {
            var text = "";
            $("#bed_box").find(".bed_item").each(function(index, el) {
                if ($(this).find(".bed").val() && $(this).find(".number").val()) {
                    text = text + $(this).find(".bed").val() + "|" + $(this).find(".number").val() + ",";
                }
            });
            bed_input.val(text);
        }
    })();
    (function() {
        var val = $("#room_facilitys").val();
        if (val) {
            val_arr = val.split(",");
            for (var i = 0; i < val_arr.length; i++) {
                var f = val_arr[i];
                $(".room_f[value='" + f + "']").attr("checked", true);
            }
        }
    })();
    $("#room_describ").e_input_tip({
        error: "最大可输入400个字符",
        space: "请输入房型描述(如：“便捷设施 入住全新体验。合理搭配 巧妙空间布局。都市中心 尊享繁华市景”。2000字以内)",
        rule: /^[\s\S]{0,400}$/
    });
    $("#room_remarks").e_input_tip({
        error: "最大可输入400个字符",
        space: "请输入房型描述",
        need: false,
        rule: /^[\s\S]{0,400}$/
    });
    $("#save_room_info").click(function(event) {
        if (!$(".room_item").length) {
            event.preventDefault();
            var a = $(this).e_window({
                top: 30,
                width: "auto",
                html: "<div class='red_tip_box'>请添加房型。</div>"
            });
            setTimeout(function() {
                a.e_window_kill();
            }, 5e3);
        }
    });
    $(".room_pr").e_input_tip({
        space: 0,
        rule: /^\d+$/
    });
    $(".upload_img").e_img_siz("", true);
    $(".upload_img_info").e_input_tip({
        space: "请输入图片描述",
        need: false
    });
    $(".upload_img_type").e_input_tip({
        need_text: "必须选择"
    });
    function upload_img(els) {
        els.each(function(index, el) {
            var el = $(el), box = el.parents(".from_path").find(".img_show_box"), info_box_id = "info_box" + new Date().getTime();
            var html = '<div class="upload_img_box">';
            html += '<div class="img_box"><img class="upload_img" /></div>';
            html += '<p class="img_set">';
            html += '<input type="text" class="upload_img_info">';
            html += '<select class="upload_img_type" name="img_type">';
            html += "</select>";
            html += "</p>";
            html += '<p class="img_del"><a href="">删除</a></p>';
            html += "</div>";
            var upload_tip = '<div class="upload_img_box">';
            upload_tip += '<div class="img_box">';
            upload_tip += "<p style=“margin-top: 20px;”></p>";
            upload_tip += "</div>";
            upload_tip += "</div>";
            el.AjaxFileUpload({
                action: "/help/FileHandle.ashx",
                onSubmit: function(filename) {
                    info_box = $(upload_tip).appendTo(box).attr("id", info_box_id);
                    info_box.find("p").html(filename + "<br><br>正在上传中...");
                    return {
                        roomid: this.attr("room_id")
                    };
                },
                onComplete: function(filename, response) {
                    for (var i = 0; i < response.length; i++) {
                        var img = response[i];
                        var a = $(html).insertBefore("#" + info_box_id);
                        a.find("img").attr("src", img.tURL).attr("oURL", img.oURL).e_img_siz("", true);
                        if (img.PID) {
                            a.attr("pid", img.PID);
                            a.find("select").html($("#img_type_sel").html());
                            a.find(".upload_img_info").e_input_tip({
                                space: "请输入图片描述",
                                need: false
                            });
                            a.find(".upload_img_type").e_input_tip({
                                need_text: "必须选择"
                            });
                        } else {
                            a.attr("pid", "error");
                            a.find(".img_set").addClass("col_red").html(img.Message);
                        }
                    }
                    $("#" + info_box_id).remove();
                },
                onError: function(filename) {
                    $("#" + info_box_id).find("p").html(filename + "<br><br>正在上传失败...");
                }
            });
        });
    }
    upload_img($(".upload_img_input"));
    $("#add_img").on("focusout", ".upload_img_info", function(event) {
        var el = $(this), pid = el.parents(".upload_img_box").attr("pid"), v = $(this).val(), ajax_load = "";
        if (v && v != el.attr("last_value")) {
            if (v == "请输入图片描述") {
                v = "";
            }
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
                } else {
                    el.attr("last_value", v);
                }
            }).fail(function(data) {
                alert("描述提交错误！错误代码：" + data.status + "," + data.statusText + "。");
            });
        }
    });
    $("#add_img").on("change", ".upload_img_type", function(event) {
        var el = $(this), pid = el.parents(".upload_img_box").attr("pid"), v = el.val();
        if (v) {
            $(this).prop("disabled", true);
            $.ajax({
                url: "/ImageProperty/ImageType",
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
        var box = $(this).parents(".upload_img_box"), pid = box.attr("pid"), t_url = box.find(".upload_img").attr("src"), o_url = box.find(".upload_img").attr("oURL"), data = {
            PID: pid == "error" ? 0 : pid,
            text1: t_url,
            text2: o_url
        };
        $.ajax({
            url: "/ImageProperty/ImageDel",
            type: "GET",
            data: data
        }).done(function(data) {
            if (data == 0) {
                alert("删除图片失败");
            } else {
                box.find(".upload_img_info,.upload_img_type").e_window_kill().unbind("input_tip_checking").attr("not_validate", "true");
                box.remove();
            }
        }).fail(function(data) {
            alert("删除图片错误！错误代码：" + data.status + "," + data.statusText + "。");
        });
    });
    $("#hotel_switch_my_drr").change(function(event) {
        console.log(event);
        window.location.href = "/DrrRule/MyDrr?id=" + $(this).find("option:selected").val();
    });
    $("#drr_name").e_input_tip({
        space: "促销价格的名称",
        check: true,
        rule: function(success_callback, error_callback, val) {
            var el = $(this);
            if (!val.match(/^[\s\S]{3,}$/)) {
                error_callback("请输入三个以上的字符", el);
                return;
            }
            $.ajax({
                url: "/DrrRule/IsOk/",
                dataType: "text",
                data: {
                    text: val,
                    id: $("#hotel_id").val()
                }
            }).done(function(data) {
                if (data == 0) {
                    error_callback("此促销价格的名称已存在", el);
                } else {
                    success_callback(el);
                }
            }).fail(function(data) {
                alert("服务器验证公寓销价名称失败");
            });
        }
    });
    var drr_modes = $("#drr_modes_hide").find(".input_line"), drr_mode = $(".drr_mode");
    drr_mode.find("input").e_input_tip();
    $(".drr_modes").change(function(event) {
        drr_mode.find("input").e_window_kill().attr("not_validate", "true");
        drr_mode.html(drr_modes.eq(this.selectedIndex).clone(false, false));
        drr_mode.find("input").e_input_tip();
    });
    $("#hotel_switch_gift").change(function(event) {
        console.log(event);
        window.location.href = "/Gift/MyGift?id=" + $(this).find("option:selected").val();
    });
    $("#GiftContent").e_input_tip();
    $("#rooms").e_input_tip({
        space_callback: function() {
            alert("请选择适用房型");
        }
    });
    $("#hotel_switch_my_guarantee").change(function(event) {
        console.log(event);
        window.location.href = "/Guarantee/MyGuarantee?id=" + $(this).find("option:selected").val();
    });
    $(".g_ru_change").click(function(event) {
        if ($(this).index(".g_ru_change") == 1) {
            $("#notify_time").e_input_tip().removeAttr("not_validate");
        } else {
            $("#notify_time").e_window_kill().unbind("input_tip_checking").attr("not_validate", "true");
        }
    });
    $(".MyGuarantee_btn").click(function(event) {
        event.preventDefault();
        var el = $(".g_ru_change:checked"), val = $(".security_costs:checked").next().text() + "|";
        if (el.index(".g_ru_change") == 1) {
            val = val + "允许变更/取消,需在最早到店时间之前" + $("#notify_time").val() + "小时通知";
        } else {
            val = val + "不许变更/取消";
        }
        $(".MyGuarantee_Description").val(val);
        $(this).parents("form").submit();
    });
    $("#hotel_switch_my_price").change(function(event) {
        console.log(event);
        window.location.href = setUrlParam("id", $(this).find("option:selected").val(), location.href);
    });
    (function() {
        if (!$("#date_load").length) {
            return;
        }
        var start = {
            elem: "#date_load",
            min: laydate.now(),
            istoday: true,
            choose: function(datas) {
                console.log(datas);
                location.href = setUrlParam("startDate", datas, location.href);
            }
        };
        laydate(start);
    })();
    (function() {
        var el_ing = "", send_data = {};
        $(".item_pr").click(function(event) {
            if (el_ing) {
                el_ing.e_window_kill();
            }
            var el = $(this), html = $("#pr_set_box").clone(false, false).removeClass("hide");
            html.find(".date_start").val(el.attr("date"));
            html.find(".date_end").val(el.attr("date"));
            html.find(".only_integer").val(el.text());
            var status = html.find(".status_val");
            if (status.length) {
                status.val(el.text().split("/")[1].replace(/(^\s*)|(\s*$)/g, ""));
                if (el.is(".grey ")) {
                    html.find("[name=r_stats]")[1].checked = true;
                } else {
                    html.find("[name=r_stats]")[0].checked = true;
                }
            }
            send_data.id = el.attr("Hotel_id");
            send_data.roomId = el.attr("roomid");
            el_ing = el.e_window({
                position_mod: "relative",
                relative_mod: "bottom",
                top: 0,
                left: 0,
                width: 500,
                marginTop: 0,
                marginRight: 0,
                box_id: "set_pr_box",
                html: html
            });
            $(".close_win").click(function(event) {
                event.preventDefault();
                el_ing.e_window_kill();
            });
            function set_date() {
                var start = {
                    elem: "#set_pr_box .date_start",
                    min: laydate.now(),
                    istoday: false,
                    choose: function(datas) {
                        end.min = datas;
                        end.start = datas;
                    }
                };
                var end = {
                    elem: "#set_pr_box .date_end",
                    min: laydate.now(),
                    istoday: false,
                    choose: function(datas) {
                        start.max = datas;
                    }
                };
                laydate(start);
                laydate(end);
            }
            setTimeout(set_date, 100);
        });
        $("body").on("click", ".set_pr_btn", function(event) {
            event.preventDefault();
            var box = $(this).parents(".set_box");
            send_data.startDate = box.find(".date_start").val();
            send_data.EndDate = box.find(".date_end").val();
            send_data.value = box.find(".only_integer").val();
            console.log(send_data);
            $.ajax({
                url: "/price/uPrice/",
                type: "GET",
                data: send_data
            }).done(function(data) {
                console.log(data);
                if (data == 1) {
                    location.reload();
                } else {
                    alert("修改失败！");
                }
            }).fail(function() {
                alert("服务器错误！");
                console.log("服务器错误！");
            });
        });
        $("body").on("click", ".set_status_btn", function(event) {
            event.preventDefault();
            var box = $(this).parents(".set_box");
            send_data.startDate = box.find(".date_start").val();
            send_data.EndDate = box.find(".date_end").val();
            send_data.CanSell = box.find(".only_integer").val();
            send_data.status = box.find("[name=r_stats]:checked").val();
            console.log(send_data);
            $.ajax({
                url: "/RStatus/uStatus/",
                type: "GET",
                data: send_data
            }).done(function(data) {
                console.log(data);
                if (data == 1) {
                    location.reload();
                } else {
                    alert("修改失败！");
                }
            }).fail(function() {
                alert("服务器错误！");
                console.log("服务器错误！");
            });
        });
    })();
    (function() {
        var el_ing = "", send_data = {};
        $(".operation_order").click(function(event) {
            event.preventDefault();
            if (el_ing) {
                el_ing.e_window_kill();
            }
            var el = $(this), html = $(".set_box").clone(false, false).removeClass("hide");
            html.find(".order_id").val(el.attr("order_id"));
            el_ing = el.e_window({
                position_mod: "relative",
                relative_mod: "bottom",
                top: 0,
                left: 0,
                width: 500,
                marginTop: 0,
                marginRight: 0,
                box_id: "",
                html: html
            });
            $(".close_win").click(function(event) {
                event.preventDefault();
                el_ing.e_window_kill();
            });
        });
    })();
    $("body").on("click", ".modify_order_status_btn", function(event) {
        event.preventDefault();
        var form = $(this).parents("form"), data = form.serialize();
        console.log(data);
        form.submit();
    });
    $("body").on("click", ".confirmation_order", function(event) {
        var el = $(this), box = el.parents(".set_box"), rejection_box = box.find(".rejection_box");
        if (box.find(".confirmation_order").index(el) == 1) {
            rejection_box.show();
        } else {
            rejection_box.hide();
        }
    });
    $("body").on("click", ".rejection_radio", function(event) {
        var el = $(this), box = el.parents(".rejection_box"), rejection_text = box.find(".rejection_text");
        if (box.find(".rejection_radio").index(el) == 2) {
            rejection_text.show();
        } else {
            rejection_text.hide();
        }
    });
    $("#bill_hotel").change(function(event) {
        window.location.href = "/Bill/QureyBill?hotelId=" + $(this).find("option:selected").val();
    });
    $("#bill_btn").click(function(event) {
        event.preventDefault();
        window.location.href = "/Bill/QureyBill?hotelId=" + $("#bill_hotel").find("option:selected").val() + "&startTime=" + $(".date_start").val() + "&endTime=" + $(".date_end").val();
    });
    $("#comment_hotel").change(function(event) {
        window.location.href = "/Comment/QueryComment?hotelId=" + $(this).find("option:selected").val();
    });
    $("#comment_btn").click(function(event) {
        event.preventDefault();
        window.location.href = "/Comment/QueryComment?hotelId=" + $("#comment_hotel").find("option:selected").val() + "&startTime=" + $(".date_start").val() + "&endTime=" + $(".date_end").val() + "&IsReply=" + $("#IsReply").val();
    });
    $(".show_comment_reply").click(function(event) {
        event.preventDefault();
        var el = $(this);
        if (el.text() == "回复") {
            el.parents("tr").find(".comment_p").show(800);
            el.text("收起回复");
        } else {
            el.parents("tr").find(".comment_p").hide(800);
            el.text("回复");
        }
    });
    $(".comment_submit_btn").click(function(event) {
        event.preventDefault();
        $(this).parents("form").submit();
    });
    $("#complaint_contact,#complaint_content").e_input_tip();
    $(function() {
        if ($("#word_failure").val() === 0) {
            alert("操作失败！");
        }
    });
    (function($) {
        function set_val(input) {
            var data_arr = input.val().split(","), multiple = input.parent().find(".multiple");
            for (var i = 0; i < data_arr.length; i++) {
                var val = data_arr[i];
                multiple.filter("[value=" + val + "]").attr("checked", "true");
            }
        }
        function set_val_b(input) {
            var data_arr = input.val().split("、"), label = input.next().find(".multiple").next();
            for (var i = 0; i < data_arr.length; i++) {
                var val = data_arr[i];
                label.filter(function() {
                    return $(this).text() == val;
                }).prev().attr("checked", "true");
            }
        }
        var rooms = $("#rooms"), rooms_text = $("#rooms_text");
        if (rooms.length) {
            set_val(rooms);
            set_val_b(rooms_text);
        }
    })($);
    $(".multiple").change(function(event) {
        var checkbox_box = $(this).parents(".checkbox_box"), input = $(this).parents(".input_line").prev(".hide"), vals = "", texts = "";
        if (!input.length) {
            input = $(this).parents(".input_line").find(".hide").eq(0);
            var input_text = input.next(".hide");
        }
        checkbox_box.find(".multiple").each(function(index, el) {
            if ($(this).attr("checked")) {
                vals += $(this).val() + ",";
                texts += $(this).next().text() + "、";
            }
        });
        vals = vals.slice(0, -1);
        texts = texts.slice(0, -1);
        input.val(vals);
        console.log(input.val());
        if (input_text) {
            input_text.val(texts);
            console.log(input_text.val());
        }
    });
    $(".all_set").click(function(event) {
        event.preventDefault();
        $(this).parents(".checkbox_box").find(".multiple").each(function(index, el) {
            $(this).attr("checked", "").change();
        });
    });
    $(".reverse_set").click(function(event) {
        event.preventDefault();
        $(this).parents(".checkbox_box").find(".multiple").each(function(index, el) {
            if ($(this).attr("checked")) {
                $(this).removeAttr("checked");
            } else {
                $(this).attr("checked", "");
            }
            $(this).change();
        });
    });
    (function($) {
        $(".multiple2").change(function(event) {
            var checkbox_box = $(this).parents(".checkbox_box"), input_values = checkbox_box.find(".multiple_values"), input_texts = checkbox_box.find(".multiple_texts"), vals = "", texts = "";
            checkbox_box.find(".multiple2").each(function(index, el) {
                if ($(this).attr("checked")) {
                    vals += $(this).val() + ",";
                    texts += $(this).next().text() + "、";
                }
            });
            vals = vals.slice(0, -1);
            texts = texts.slice(0, -1);
            input_values.val(vals);
            input_texts.val(texts);
        });
        $(".all_set").click(function(event) {
            event.preventDefault();
            $(this).parents(".checkbox_box").find(".multiple2").each(function(index, el) {
                $(this).attr("checked", "").change();
            });
        });
        $(".reverse_set").click(function(event) {
            event.preventDefault();
            $(this).parents(".checkbox_box").find(".multiple2").each(function(index, el) {
                if ($(this).attr("checked")) {
                    $(this).removeAttr("checked");
                } else {
                    $(this).attr("checked", "");
                }
                $(this).change();
            });
        });
    })($);
    $("body").on("keypress", ".only_integer", function(event) {
        var key_code = event.keyCode == 0 ? event.which : event.keyCode;
        console.log(key_code);
        if (key_code >= 48 && key_code <= 59 || key_code == 8) {} else {
            event.preventDefault();
        }
    }).on("change", ".only_integer", function(event) {
        var el = $(this);
        el.val(el.val().replace(/\D/g, ""));
    });
    $("body").on("keypress", ".only_float", function(event) {
        var key_code = event.keyCode == 0 ? event.which : event.keyCode;
        console.log(key_code);
        if (key_code >= 48 && key_code <= 59 || key_code == 46) {} else {
            event.preventDefault();
        }
    }).on("change", ".only_float", function(event) {
        var el = $(this);
        el.val(el.val().replace(/[^\d\.]/g, ""));
    });
    $(".checking_btn").click(function(event) {
        event.preventDefault();
        var el = $(this), status = 0;
        var err_el = $("[rules_error]");
        $(".room_img_item").each(function(index, el) {
            if ($(this).find(".img_set").not(".col_red").length < 5) {
                status = 2;
            }
        });
        if (status == 2) {
            alert("每个房型图片不能少于5张。");
            return;
        }
        $("form").submit();
    });
    function setUrlParam(para_name, para_value, url) {
        var strNewUrl = new String();
        var strUrl = url;
        if (strUrl.indexOf("?") != -1) {
            strUrl = strUrl.substr(strUrl.indexOf("?") + 1);
            if (strUrl.toLowerCase().indexOf(para_name.toLowerCase()) == -1) {
                strNewUrl = url + "&" + para_name + "=" + para_value;
                return strNewUrl;
            } else {
                var aParam = strUrl.split("&");
                for (var i = 0; i < aParam.length; i++) {
                    if (aParam[i].substr(0, aParam[i].indexOf("=")).toLowerCase() == para_name.toLowerCase()) {
                        aParam[i] = aParam[i].substr(0, aParam[i].indexOf("=")) + "=" + para_value;
                    }
                }
                strNewUrl = url.substr(0, url.indexOf("?") + 1) + aParam.join("&");
                return strNewUrl;
            }
        } else {
            strUrl += "?" + para_name + "=" + para_value;
            return strUrl;
        }
    }
})(jQuery);
//# sourceMappingURL=main.map