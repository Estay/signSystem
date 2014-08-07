/********************************************************
 ** @itiwll
 ** 20140623
 **
 **********************************************************/
(function($) {
	var estay_sas = {};

	//////////////////////////////////新建酒店/////////////////////////////////////////////

	// 城市输入提示和验证
	$("#hotel_name").e_input_tip({
		space : "请输入公寓名称",
		rule: function(success_callback,error_callback,val) {
			var el = $(this);

			if (!val.match(/^[\s\S]{3,}$/)) {
				error_callback("请输入三个以上的字符",el);
				return ;
			};


			$.ajax({
				url: '/AddHotel/IsOk/',
				dataType: 'text',
				data: {text:val }
			})
			.done(function(data) {
				if(data==0){
					error_callback("此公寓已存在",el);
				}else{
					success_callback(el);
				}
			})
			.fail(function() {
				alert("服务器验证公寓名称失败");
			});	
		}
	});

	// 所属类别 公寓主题 地址及所在商区 选择验证和提示
	$("#hotel_class,#hotel_theme,#hotel_province,#h_city,#h_administrative_region,#h_business_zone").e_input_tip({
		need_text :"必需选择"
	});



	//电话值处理
	$("#phone_area_code,#hotel_phone").keyup(function(event) {
		$("#phone").val($("#phone_area_code").val()+"-"+$("#hotel_phone").val());
	});

	// 固定电话提示和验证
	(function($){
		var area = $("#phone_area_code"),
			fixed_phone =$("#hotel_phone");

		area.e_input_tip({
			space : "区号",
			need_text : "必填",
			error : "错误",
			rule : /^\d{3,4}$/
		});
		fixed_phone.e_input_tip({
			space : "座机号码",
			need_text : "必需填写",
			error : "格式不正确",
			rule : /^\d{7,8}$/
		});

	})($)

	// 传真
	$("#hotel_fax").e_input_tip({
		space : "传真号码(带区号)",
		need: false,
		error : "格式不正确",
		rule : /^\d{3,4}\-?\d{7,8}$/
	});
	// 手机
	$("#mobeli_phone").e_input_tip({
		space : "11位手机号码",
		need_text: "必填,接收预订信息",
		error : "格式不正确",
		rule : /^1\d{10}$/
	});
	// 地址
	$("#hotel_address").e_input_tip({
		space : "公寓详细地址",
		error : "格式不正确",
		rule : /^[\s\S]+$/
	});


	// 城市及所在商区 联动模块  回显
	(function($) {
		var province = $("#hotel_province"),
			city = $("#h_city"),
			region = $("#h_administrative_region"),
			zone = $("#h_business_zone");

		// 设置省份
		province.change(function(event) {
			seted_province();
		});

		function seted_province () {
			// 地图同步
			map.centerAndZoom(province.find(':selected').text());

			// 禁用 城市 商圈 行政区选择 
			city.add(region).add(zone).attr('disabled', '');

			if (province.val()) {
				$.ajax({
					url: '/help/location.ashx',
					type: 'GET',
					dataType: 'json',
					data : {
						type : "city",
						value: province.val()
					}
				})
				.done(function(data) {
					city.children().slice(1).remove();
					region.children().slice(1).remove();
					zone.children().slice(1).remove();

					var val = city.attr('original'),
						option = "";
					for (var i = 0; i < data.length; i++) {
						var city_data = data[i];
						option = option + '<option value="'+city_data.id+'"' + (val==city_data.id? " selected" : "") +'>'+city_data.name+'</option>';
					};
					city.append(option);

					if(val){
						city.removeClass('col_gray');
						region.removeClass('col_gray');
						zone.removeClass('col_gray');
						seted_city();
					}

					// 启用城市选择
					city.removeAttr('disabled');

				})
				.fail(function() {
					alert("加载城市数据错误");
				})
				.always(function() {
				});
			}else{
				city.children().slice(1).remove();
			}
		}

		seted_province();

		// 设置城市
		city.change(function(event) {
			seted_city();
		});

		function seted_city () {
			region.add(zone).attr('disabled', '');

			
			if(city.val()){

				//地图同步
				map.centerAndZoom(city.find(':selected').text());

				// 加载行政区数据
				$.ajax({
					url: '/help/location.ashx',
					type: 'GET',
					dataType: 'json',
					data : {
						type : "region",
						value: city.val()
					}
				})
				.done(function(data) {
					region.children().slice(1).remove();

					var val = region.attr('original'),
						option = "";
					for (var i = 0; i < data.length; i++) {
						var city_data = data[i];
						option = option + '<option value="'+city_data.id+'"' + (val==city_data.id ? " selected" : "") +'>'+city_data.name+'</option>';
					};
					region.append(option);
				})
				.fail(function() {
					alert("加载行政区数据错误");
				})
				.always(function() {
				});

				// 加载商圈数据
				$.ajax({
					url: '/help/location.ashx',
					type: 'GET',
					dataType: 'json',
					data : {
						type : "commercial",
						value: city.val()
					}
				})
				.done(function(data) {
					zone.children().slice(1).remove();

					var val = zone.attr('original'), 
					option = "";
					for (var i = 0; i < data.length; i++) {
						var city_data = data[i];
						option = option + '<option value="'+city_data.id+'"' + (val==city_data.id ? " selected" : "") +'>'+city_data.name+'</option>';
					};
					zone.append(option);
				})
				.fail(function() {
					alert("加载商圈数据错误");
				})
				.always(function() {
				});

				// 启用行政区商圈选择
				region.add(zone).removeAttr('disabled');
			}else {
					// region.add(zone).each(function(index, el) {
					// 	el.children().slice(1).remove();
					// });
				console.log("message");

			}
		}

		// 设置商圈和行政区
		region.add(zone).change(function(event) {
			map.centerAndZoom(city.find(':selected').text());
		});
		

	})($);


	// 地图输入方式切换
	$("#location_box").e_tab_switch({
		callback: function(index) {
			if (index == 0) {
				$("#map_lon,#map_lat").e_window_kill();
			} else {
				// todo 地图验证逻辑
			}
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
		space : "输入经度",
		need: false,
		error : "格式不正确",
		rule : /^\-{0,1}\d{1,3}$|^\-{0,1}\d{3}.\d+$/		
	});
	$("#map_lat").e_input_tip({
		space : "输入维度",
		need: false,
		error : "格式不正确",
		rule : /^\-{0,1}\d{1,3}$|^\-{0,1}\d{3}.\d+$/
	});

	// 触发图片选择
	// $(".upload_img_box").on('click', '.upload_img_btn', function(event) {
	// 	event.preventDefault();
	// 	$(this).siblings('.upload_img_input').click();
	// });

	// 公寓楼高 房间总数
	$("#hotel_building,#hotel_room_count").e_input_tip({
		space : "0",
		error : "格式不正确",
		rule : /^\d+$/
	});
	// 开业时间 装修时间
	$("#hotel_built_year,#hotel_decoration_time_year").siblings('select').change(function(event) {
		var p = $(this).parent(), 
			val = p.find('.select_yeae').val()+p.find('.select_month').val();
		p.find('input.hide').val(val);
	});

	// 公寓特色
	$("#hotel_specialty").e_input_tip({
		space : "请输入公寓的特色",
		need : false,
		error : "最大可输入2000个字符",
		rule : /^[\S\s]{0,2000}$/,
		error_callback : function (error,el) {
				$(this).e_window({
					relative_mod : "right",
					left : 10,
					width: "auto",
					html: "<div class='red_tip_box'>"+error+"</div>"
				})
			}
	});

	// 公寓简介
	$("#hotel_abstract").e_input_tip({
		space : "请输入公寓简介",
		error : "最大可输入2000个字符",
		rule : /^[\S\s]{0,2000}$/,
		error_callback : function (error,el) {
				$(this).e_window({
					relative_mod : "right",
					left : 30,
					width: "auto",
					html: "<div class='red_tip_box'>"+error+"</div>"
				})
			}
	});

	// 交通位置
	$("#hotel_place").e_input_tip({
		space : "请输入交通位置",
		error : "最大可输入2000个字符",
		error : "最大可输入2000个字符",
		rule : /^[\S\s]{0,2000}$/,
		error_callback : function (error,el) {
				$(this).e_window({
					relative_mod : "right",
					left : 30,
					width: "auto",
					html: "<div class='red_tip_box'>"+error+"</div>"
				})
			}
	});


	//公寓配套 公寓服务 回显
	(function($){
		function set_val (input) {
			var data_arr = input.val().split(","),
				multiple = input.next().find('.multiple');

			for (var i = 0; i < data_arr.length; i++) {
				var val = data_arr[i];
				multiple.filter('[value='+val+']').attr('checked', 'true');
			};
		}

		function set_val_b (input) {
			var data_arr = input.val().split("、"),
				label = input.next().find('.multiple').next();

			for (var i = 0; i < data_arr.length; i++) {
				var val = data_arr[i];
				label.filter(function(){
				   return $(this).text() == val;
				}).prev().attr('checked', 'true');
			};
		}

		var f_input = $("#facilities_hide"),
			s_input = $("#generalAmenities_hide");

		set_val(f_input);
		set_val_b(s_input);

	})($)

	

	//////////////////////////////////添加房型/////////////////////////////////////////////
	// 房型名称
	$("#room_name").e_input_tip({
			space :"请输入房型名称",
			rule : /^[\s\S]{2,}$/
	});

	// 数量
	$("#room_count").e_input_tip({
			space :0,
			rule : /^\d+$/
	});

	// 宜住人数
	$("#people_number").e_input_tip({
		need_text:"必须选择"
	});

	// 面积
	$("#room_area").e_input_tip({
		space :0,
		rule : /^\d+$/
	});

	// 楼层
	$("#room_floor").e_input_tip({
		space : "",
		rule : /^[\s\S]+$/
	});

	// 添加床型
	$(".bed_add").click(function(event) {
		event.preventDefault();
		var clone = $(".bed_item.hide").clone().removeClass('hide');
		$(this).before(clone);
	});



	(function() {
		var bed_input = $("#bed_input"),
			bed_val = bed_input.val();

		if (bed_val) {
			var bed_arr = bed_val.split(",");
			$(".bed_item").eq(1).remove();
			for (var i = 0; i < bed_arr.length; i++) {
				if(bed_arr[i]){
					var bed = bed_arr[i].split("|");
					var clone = $(".bed_item.hide").clone().removeClass('hide');
					clone.find(".bed").val(bed[0]);
					clone.find(".number").val(bed[1]);
					$(".bed_add").before(clone);
				}
			};
		};

		// 删除床型
		$("#bed_box").on('click', '.bed_del', function(event) {
			event.preventDefault();
			$(this).parents(".bed_item").remove();
			setBedInput();
		});

		$("#bed_box").on('change', 'select', function(event) {
			// var n = 0;
			setBedInput();
		});

		function setBedInput () {
			var text = "";
			$("#bed_box").find('.bed_item').each(function(index, el) {
				if ($(this).find('.bed').val()&&$(this).find('.number').val()) {
					text = text + $(this).find('.bed').val()+"|"+$(this).find('.number').val()+",";
				};
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
				$(".room_f[value='"+ f + "']").attr("checked", true);
			};
		};

	})();


	// 房型描述

	$("#room_describ").e_input_tip({
		error : "最大可输入2000个字符",
		space:"请输入房型描述",
		rule: /^[\s\S]{0,2000}$/
	});

	// 房型备注
	$("#room_remarks").e_input_tip({
		error : "最大可输入2000个字符",
		space:"请输入房型描述",
		need: false,
		rule: /^[\s\S]{0,2000}$/
	})


	// 保存房型
	$("#save_room_info").click(function(event) {
		if (!$(".room_item").length) {
			event.preventDefault();
			var a = $(this).e_window({
				top : 30,
				width: "auto",
				html: "<div class='red_tip_box'>请添加房型。</div>"
			});
			setTimeout(function() {
				a.e_window_kill();
			}, 5000);
		}
	});



	/////////////////////////////房价///////////////////////////////////////
	$(".room_pr").e_input_tip({
		space : 0,
		rule : /^\d+$/
	})
	

	///////////////////////////////////////上传图片/////////////////////////
	function upload_img(els) {
		els.each(function(index, el) {
			var el = $(el),
				box = el.parents(".from_path").find('.img_show_box'),
				info_box = "";

			var	html =			'<div class="upload_img_box">';
				html +=				'<div class="img_box"><img class="upload_img" /></div>';
				html +=				'<p class="img_set">';
				html +=					'<input type="text" class="upload_img_info">';
				html +=					'<select class="upload_img_type" name="img_type">';
				html +=					'</select>';
				html +=				'</p>';
				html +=				'<p class="img_del"><a href="">删除</a></p>';
				html +=			'</div>';

			var upload_tip =  '<div class="upload_img_box">';
				upload_tip +=		'<div class="img_box">';
				upload_tip +=			'<p style=“margin-top: 20px;”></p>';
				upload_tip +=		'</div>';
				upload_tip += '</div>';


			el.AjaxFileUpload({
				action: "/help/FileHandle.ashx",
				onSubmit: function(filename) {

					info_box = $(upload_tip).appendTo(box);
					info_box.find('p').text(filename+"正在上传中...");

					return {
						roomid: this.attr('room_id')
					}
				},
				onComplete: function(file, response) {
					// alert(JSON.stringify(response));
					for (var i = 0; i < response.length; i++) {
						var img = response[i];
						var a = $(html).appendTo(box);

						a.find('img').attr('src', img.URL).e_img_siz("",true);
						if (img.PID) {
							a.data('pid', img.PID);
							a.find('select').html($("#img_type_sel").html());
						}else {
							a.data('pid', "error");
							a.find('.img_set').addClass('col_red').html(img.Message);
						}
						
					};
					info_box.remove();
				}
			});
		});
	}

	upload_img($(".upload_img_input"));

	// 设置图片描述
	$("#add_img").on('focusout',".upload_img_info", function(event) {
		var pid = $(this).parents(".upload_img_box").data('pid'),
			v = $(this).val(),
			ajax_load = "";


		if (v) {

			//提交描述
			ajax_load = $.ajax({
				url: '/ImageProperty/ImageDes',
				type: 'GET',
				data: {
					PID: pid,
					text: v}
			})
			.done(function(data) {
				if (data==0) {
					alert("描述提交失败");
				};
			})
			.fail(function(data) {
				alert("描述提交错误！错误代码：" + data.status + ","+data.statusText + "。");
			});
		};
	});

	// 设置图片类型
	$("#add_img").on('change',".upload_img_type", function(event) {
		var el = $(this),
			pid = el.parents(".upload_img_box").data('pid'),
			v = el.val();

		if (v) {
			$(this).prop('disabled',true);

			//提交类型
			$.ajax({
				url: '/ImageProperty/ImageDes',
				type: 'GET',
				data: {
					PID: pid,
					text : v
				}
			})
			.done(function(data) {
				if (data==0) {
					alert("设置图片类型失败");
					el[0].selectedIndex=0;
				};
			})
			.fail(function(data) {
				alert("提交图片类型错误！错误代码："+data.status+","+data.statusText+"。");
			})
			.always(function() {
				el.removeAttr('disabled');
			});
		};
	});





	// 删除图片按钮
	$("#add_img").on('click', '.img_del a', function(event) {
		event.preventDefault();
		var box = $(this).parents(".upload_img_box"),
			pid = box.data('pid'),
			url = box.find('.upload_img').attr('src'),
			data = {
				PID : pid=="error"?0:pid,
				text : url
			};

		//删除图片请求
		$.ajax({
			url: '/ImageProperty/ImageDel',
			type: 'GET',
			data: data
		})
		.done(function(data) {
			if (data==0) {
				alert("删除图片失败");
			}else {
				box.remove();
			}
		})
		.fail(function(data) {
			alert("删除图片错误！错误代码："+data.status+","+data.statusText+"。");
		});
		
	});


	//////////////////////////////////功能按钮//////////////////////////////////////	

	//多选值处理
	$(".multiple").change(function(event) {
		var checkbox_box = $(this).parents(".checkbox_box"),

			input = $(this).parents(".input_line").prev(".hide"),
			vals = "";
		input = input.length?input : $(this).parents(".input_line").find(".hide");
		checkbox_box.find('.multiple').each(function(index, el) {
			if ($(this).attr('checked')) {
				vals += ($(this).val()+",");
			};
		});
		vals = vals.slice(0, -1);
		input.val(vals);
	});
	// 全选
	$(".all_set").click(function(event) {
		event.preventDefault();
		$(this).parents(".checkbox_box").find('.multiple').each(function(index, el) {
			$(this).attr('checked',"").change();
		});
	});
	// 反选
	$(".reverse_set").click(function(event) {
		event.preventDefault();
		$(this).parents(".checkbox_box").find('.multiple').each(function(index, el) {
			if($(this).attr('checked')){
				$(this).removeAttr('checked');
			}else {
				$(this).attr('checked',"");
			}
			$(this).change();
		});
	});


	// 验证表单
	$(".checking_btn").click(function(event) {
		event.preventDefault();
		var el = $(this),
			status = 0;
		var input = $(this).parents(".box_a").find('input[type=text],select[name],textarea[name]').trigger("input_tip_checking");
		setTimeout(function() {
			input.each(function(index, el) {
				if ($(this).attr('rules_error')||$(this).attr('rules_error')=="") {
					status = 1;
					return false;
				};
			});
			$(".room_img_item").each(function(index, el) {
				if($(this).find('.img_set').length<5){
					status = 2;
				}
			});
			$("#bed_input").each(function() {
				if(!$(this).val()){
					status = 3;
				}
			});
			if (status == 0) {
				document.forms[0].submit();
			}else {
				if(status == 1){
					var Message = "填写的信息没有通过验证，请检查。";
				};
				if(status == 2){
					var Message = "每个房型图片不能少于5张。";
				};
				if(status == 3) {
					var Message = "必须设置床型";
				};
				el.e_window({
					relative_mod : "right",
					left : 30,
					width: "auto",
					html: "<div class='red_tip_box'>"+ Message +"</div>"
				});
				setTimeout(function() {
					el.e_window_kill();
				}, 5000);
			};				
		}, 200);
	});

})(jQuery);


