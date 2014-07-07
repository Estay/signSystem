/*2014年7月7日15:40:47*/
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
    $.fn.ajaxfileupload = function(options) {
        var settings = {
            params: {},
            action: "",
            onStart: function() {
                console.log("starting upload");
                console.log(this);
            },
            onComplete: function(response) {
                console.log("got response: ");
                console.log(response);
                console.log(this);
            },
            onCancel: function() {
                console.log("cancelling: ");
                console.log(this);
            },
            validate_extensions: true,
            valid_extensions: [ "gif", "png", "jpg", "jpeg" ],
            submit_button: null
        };
        var uploading_file = false;
        if (options) {
            $.extend(settings, options);
        }
        return this.each(function() {
            var $element = $(this);
            if ($element.data("ajaxUploader-setup") === true) return;
            $element.change(function() {
                uploading_file = false;
                if (settings.submit_button == null) {
                    upload_file();
                }
            });
            if (settings.submit_button == null) {} else {
                settings.submit_button.click(function(e) {
                    e.preventDefault();
                    if (!uploading_file) {
                        upload_file();
                    }
                });
            }
            var upload_file = function() {
                if ($element.val() == "") return settings.onCancel.apply($element, [ settings.params ]);
                var ext = $element.val().split(".").pop().toLowerCase();
                if (true === settings.validate_extensions && $.inArray(ext, settings.valid_extensions) == -1) {
                    settings.onComplete.apply($element, [ {
                        status: false,
                        message: "The select file type is invalid. File must be " + settings.valid_extensions.join(", ") + "."
                    }, settings.params ]);
                } else {
                    uploading_file = true;
                    wrapElement($element);
                    var ret = settings.onStart.apply($element, [ settings.params ]);
                    if (ret !== false) {
                        $element.parent("form").submit(function(e) {
                            e.stopPropagation();
                        }).submit();
                    }
                }
            };
            $element.data("ajaxUploader-setup", true);
            var handleResponse = function(loadedFrame, element) {
                var response, responseStr = loadedFrame.contentWindow.document.body.innerHTML;
                try {
                    response = JSON.parse(responseStr);
                } catch (e) {
                    response = responseStr;
                }
                element.siblings().remove();
                element.unwrap();
                uploading_file = false;
                settings.onComplete.apply(element, [ response, settings.params ]);
            };
            var wrapElement = function(element) {
                var frame_id = "ajaxUploader-iframe-" + Math.round(new Date().getTime() / 1e3);
                $("body").after('<iframe width="0" height="0" style="display:none;" name="' + frame_id + '" id="' + frame_id + '"/>');
                $("#" + frame_id).load(function() {
                    handleResponse(this, element);
                });
                element.wrap(function() {
                    return '<form action="' + settings.action + '" method="POST" enctype="multipart/form-data" target="' + frame_id + '" />';
                }).before(function() {
                    var key, html = "";
                    for (key in settings.params) {
                        var paramVal = settings.params[key];
                        if (typeof paramVal === "function") {
                            paramVal = paramVal();
                        }
                        html += '<input type="hidden" name="' + key + '" value="' + paramVal + '" />';
                    }
                    return html;
                });
            };
        });
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
    $(".upload_img_input").ajaxfileupload({
        action: "/help/FileHandle.ashx",
        onComplete: function(response) {
            console.log("custom handler for file:");
            alert(JSON.stringify(response));
        }
    });
})(jQuery);
//# sourceMappingURL=main.map