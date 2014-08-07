/*2014年8月7日13:44:33*/
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
            }).keyup(function(event) {
                var el = $(this);
                if (el.val() == settings.space) {
                    el.addClass("col_gray");
                } else {
                    el.removeClass("col_gray");
                }
            }).bind("input_tip_checking", function() {
                var el = $(this);
                if (el.val() == "" || el.val() == settings.space) {
                    init(el);
                    if (settings.need) {
                        settings.error_callback.call(this, settings.need_text, el);
                    }
                } else {
                    ruleValidate(el, el.val());
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
                settings.error_callback.call(el[0], error, el);
            }
            function success(el) {
                el.removeAttr("rules_error");
                settings.success_callback.call(el[0], el);
            }
            function ruleValidate(el, val) {
                if (/[\<\>\&]+/.exec(val)) {
                    error(el, "不能包含“<”,“>”,“&”等特殊字符");
                    return;
                }
                if (!settings.rule) {
                    success(el);
                    return;
                }
                if (isRegExp(settings.rule)) {
                    if (!settings.rule.exec(val)) {
                        error(el, settings.error);
                    } else {
                        success(el);
                    }
                } else if (settings.rule instanceof Function) {
                    settings.rule.call(el[0], function(el) {
                        success(el);
                    }, function(error_text, el) {
                        error(el, error_text ? error_text : settings.error);
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
        rule: function(success_callback, error_callback, val) {
            var el = $(this);
            if (!val.match(/^[\s\S]{3,}$/)) {
                error_callback("请输入三个以上的字符", el);
                return;
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
    $("#phone_area_code,#hotel_phone").keyup(function(event) {
        $("#phone").val($("#phone_area_code").val() + "-" + $("#hotel_phone").val());
    });
    (function($) {
        var area = $("#phone_area_code"), fixed_phone = $("#hotel_phone");
        area.e_input_tip({
            space: "区号",
            need_text: "必填",
            error: "错误",
            rule: /^\d{3,4}$/
        });
        fixed_phone.e_input_tip({
            space: "座机号码",
            need_text: "必需填写",
            error: "格式不正确",
            rule: /^\d{7,8}$/
        });
    })($);
    $("#hotel_fax").e_input_tip({
        space: "传真号码(带区号)",
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
        var province = $("#hotel_province"), city = $("#h_city"), region = $("#h_administrative_region"), zone = $("#h_business_zone");
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
    })($);
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
        rule: /^\-{0,1}\d{1,3}$|^\-{0,1}\d{3}.\d+$/
    });
    $("#map_lat").e_input_tip({
        space: "输入维度",
        need: false,
        error: "格式不正确",
        rule: /^\-{0,1}\d{1,3}$|^\-{0,1}\d{3}.\d+$/
    });
    $("#hotel_building,#hotel_room_count").e_input_tip({
        space: "0",
        error: "格式不正确",
        rule: /^\d+$/
    });
    $("#hotel_built_year,#hotel_decoration_time_year").siblings("select").change(function(event) {
        var p = $(this).parent(), val = p.find(".select_yeae").val() + p.find(".select_month").val();
        p.find("input.hide").val(val);
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
        space: "请输入公寓简介",
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
        space: "请输入交通位置",
        error: "最大可输入2000个字符",
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
        set_val(f_input);
        set_val_b(s_input);
    })($);
    $("#room_name").e_input_tip({
        space: "请输入房型名称",
        rule: /^[\s\S]{2,}$/
    });
    $("#room_count").e_input_tip({
        space: 0,
        rule: /^\d+$/
    });
    $("#people_number").e_input_tip({
        need_text: "必须选择"
    });
    $("#room_area").e_input_tip({
        space: 0,
        rule: /^\d+$/
    });
    $("#room_floor").e_input_tip({
        space: "",
        rule: /^[\s\S]+$/
    });
    $(".bed_add").click(function(event) {
        event.preventDefault();
        var clone = $(".bed_item.hide").clone().removeClass("hide");
        $(this).before(clone);
    });
    (function() {
        var bed_input = $("#bed_input"), bed_val = bed_input.val();
        if (bed_val) {
            var bed_arr = bed_val.split(",");
            $(".bed_item").eq(1).remove();
            for (var i = 0; i < bed_arr.length; i++) {
                if (bed_arr[i]) {
                    var bed = bed_arr[i].split("|");
                    var clone = $(".bed_item.hide").clone().removeClass("hide");
                    clone.find(".bed").val(bed[0]);
                    clone.find(".number").val(bed[1]);
                    $(".bed_add").before(clone);
                }
            }
        }
        $("#bed_box").on("click", ".bed_del", function(event) {
            event.preventDefault();
            $(this).parents(".bed_item").remove();
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
        error: "最大可输入2000个字符",
        space: "请输入房型描述",
        rule: /^[\s\S]{0,2000}$/
    });
    $("#room_remarks").e_input_tip({
        error: "最大可输入2000个字符",
        space: "请输入房型描述",
        need: false,
        rule: /^[\s\S]{0,2000}$/
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
            var upload_tip = '<div class="upload_img_box">';
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
    $(".multiple").change(function(event) {
        var checkbox_box = $(this).parents(".checkbox_box"), input = $(this).parents(".input_line").prev(".hide"), vals = "";
        input = input.length ? input : $(this).parents(".input_line").find(".hide");
        checkbox_box.find(".multiple").each(function(index, el) {
            if ($(this).attr("checked")) {
                vals += $(this).val() + ",";
            }
        });
        vals = vals.slice(0, -1);
        input.val(vals);
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
    $(".checking_btn").click(function(event) {
        event.preventDefault();
        var el = $(this), status = 0;
        var input = $(this).parents(".box_a").find("input[type=text],select[name],textarea[name]").trigger("input_tip_checking");
        setTimeout(function() {
            input.each(function(index, el) {
                if ($(this).attr("rules_error") || $(this).attr("rules_error") == "") {
                    status = 1;
                    return false;
                }
            });
            $(".room_img_item").each(function(index, el) {
                if ($(this).find(".img_set").length < 5) {
                    status = 2;
                }
            });
            $("#bed_input").each(function() {
                if (!$(this).val()) {
                    status = 3;
                }
            });
            if (status == 0) {
                document.forms[0].submit();
            } else {
                if (status == 1) {
                    var Message = "填写的信息没有通过验证，请检查。";
                }
                if (status == 2) {
                    var Message = "每个房型图片不能少于5张。";
                }
                if (status == 3) {
                    var Message = "必须设置床型";
                }
                el.e_window({
                    relative_mod: "right",
                    left: 30,
                    width: "auto",
                    html: "<div class='red_tip_box'>" + Message + "</div>"
                });
                setTimeout(function() {
                    el.e_window_kill();
                }, 5e3);
            }
        }, 200);
    });
})(jQuery);
//# sourceMappingURL=main.map