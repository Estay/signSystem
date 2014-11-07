/********************************************************
 ** @itiwll
 ** 20140623
 **
 **********************************************************/
 (function (){  



})();


(function($) {

	//创建空console对象，避免JS报错  

	if(!window.console)  
	    window.console = {};  
	var console = window.console;  

	var funcs = ['assert', 'clear', 'count', 'debug', 'dir', 'dirxml',  
	             'error', 'exception', 'group', 'groupCollapsed', 'groupEnd',  
	             'info', 'log', 'markTimeline', 'profile', 'profileEnd',  
	             'table', 'time', 'timeEnd', 'timeStamp', 'trace', 'warn'];  
	for(var i=0,l=funcs.length;i<l;i++) {  
	    var func = funcs[i];  
	    if(!console[func])
	        console[func] = function(){};  
	}  
	if(!console.memory)  
	    console.memory = {};

	//////////////////////////////////登陆页面/////////////////////////////////////////////
	$("#login_id_input").e_input_tip({
		space : "用户名",
		need : true
	});



	$("#login_password_input_show").focusin(function(event) {
		$(this).hide();
		$(this).prev().show().focus();
	});
	$("#login_password_input").e_input_tip({
		space : "",
		need : true,
		space_callback : function(need_text,el) {
			el.hide().next().show()
			.e_window({
					top: 5,
					width: "auto",
					html: "<div class='red_tip_box'>"+need_text+"</div>"
				});
		},
		success_callback : function(el) {
				el.next().e_window_kill();
			},
	});
	$("#login_code_input").e_input_tip({
		space : "验证码",
		need : true
	});

	$("#login_btn").click(function(event) {
		event.preventDefault();
		$(this).parents("form").submit();
	});

	$('#re_vali_code').click(function(event) {
		event.preventDefault();
		var img = $(this).find('img').attr('src', '/Login/builImage/?date='+new Date().getTime());
		
	});

	//////////////////////////////////公寓列表/////////////////////////////////////////////
	$('.del_hotel').click(function(event) {
		var el = $(this);
		var r = confirm('确认删除公寓<'+ el.attr('hotel_name') +'>?！');
		event.preventDefault();
		if (r==true) {
			$.ajax({
				url: el[0].href,
				type: 'GET',
				data: {hotel_id: el.attr('hotel_id')},
			})
			.done(function(date) {
				console.log("success");
				if (date == 1) {
					location.reload();
				}else {
					alert("删除失败");
				}
			})
			.fail(function() {
				console.log("error");
			})
			.always(function() {
				console.log("complete");
			});
			
		};
	});



	//////////////////////////////////新建酒店/////////////////////////////////////////////

	// 城市输入提示和验证
	$("#hotel_name").e_input_tip({
		space : "请输入公寓名称",
		check : true, //失去焦点验证
		rule: function(success_callback,error_callback,val) {
			var el = $(this);

			if (!val.match(/^[\s\S]{3,}$/)) {
				error_callback("请输入三个以上的字符",el);
				return ;
			};


			$.ajax({
				url: '/Common/IsOkHotel',
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
	})
	// 匹配库中的公寓
	.autoComplete({
		url : "/common/queryHotel",
		callblack : function(data) {
			var arr = data.split("|"),
				html = "";

			for (var i = 0; i < arr.length; i++) {
				var h = arr[i],
					text = h.slice(0, h.indexOf("["));
				console.log(h);

				html = html + '<p style="background: #FFF;border-bottom: 1px solid #999;"><a href="/addhotel/FindHotel?text='+ text +'">' + h + '</a></p>';
			};
			$(this).e_window({
				position_mod: "relative", //位置模式 居中：center 相对元素 ：relative  相对窗口：absolute
				relative_mod: "bottom", //bottom right top left
				top: 5,
				left: 0,
				width: "auto",
				marginTop: 0,
				marginRight: 0,
				layer : false,
				box_id: "h_list",
				html: html
			})
		}
	});

	// 所属类别 公寓主题 地址及所在商区 选择验证和提示
	$("#hotel_class,#hotel_theme,#hotel_province,#h_city,#h_administrative_region,#h_business_zone").e_input_tip({
		need_text :"必需选择"
	});



	//电话值处理
	$("#phone_area_code,#hotel_phone").focusout(function(event) {
		console.log("电话值处理");
		$("#phone").val($("#phone_area_code").val()+"-"+$("#hotel_phone").val());
	});

	// 固定电话提示和验证
	(function($){
		var area = $("#phone_area_code"),
			fixed_phone =$("#hotel_phone"),
			area_space = "区号",
			fixed_phone_space = "座机号码";

		area.e_input_tip({
			need:false,
			space : area_space,
			need_text : "请填写区号",
			error : "格式错误",
			rule : function(success_callback,error_callback,val){
				if(val=="" || val == area_space){
					if(fixed_phone.val()!="" && fixed_phone.val()!=fixed_phone_space){
						error_callback("请填写区号");
						return false;
					}else{
						return true;
					}
				}else {
					if(/^\d{3,4}$/.exec(val)){
						success_callback();
						return true;
					}else {
						error_callback();
						return false;
					}
				}
			} 
		});

		fixed_phone.e_input_tip({
			need:false,
			space : fixed_phone_space,
			need_text : "请填写座机号码",
			error : "格式错误",
			rule : function(success_callback,error_callback,val){
				if(val=="" || val == fixed_phone_space){
					if(area.val()!="" && area.val()!=area_space){
						error_callback("请填写座机号码");
						return false;
					}else{
						return true;
					}
				}else {
					if(/^\d{7,8}$/.exec(val)){
						success_callback();
						return true;
					}else {
						error_callback();
						return false;
					}
				}
			} 
		});

	})($)

	// 传真
	$("#hotel_fax").e_input_tip({
		space : "(如:0755-83386677)",
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
		// 地图位置回显
		var lon = $('#map_lon_input').val(),
			lat = $('#map_lat_input').val();
		if (lon && lat) {
			mapClick(lon, lat);
		};

		var province = $("#hotel_province"),
			city = $("#h_city"),
			region = $("#h_administrative_region"),
			zone = $("#h_business_zone");

		if (province.length) {
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
					// todo 地图查询酒店
					// var hotel_name = $("#hotel_name").val();
					// if(hotel_name){
					// 	new BMap.LocalSearch
					// }


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
		};

		

	})($);

	// 地图模块
	(function() {
		var lon = $("#map_lon"),
			lat = $("#map_lat"),
			location_box = $("#location_box");

			lon.keyup(function(event) {
				$("#map_lon_input").val($(this).val());
				$("#map_lon_text").text($(this).val());
			});
			lat.keyup(function(event) {
				$("#map_lat_input").val($(this).val());
				$("#map_lat_text").text($(this).val());
			});
		// 地图输入方式切换
		location_box.e_tab_switch({
			callback: function(index) {
				if (index == 0) {
					// 切换到地图设置
					// todo
					$("#show_coordinates").show();
					lat.add(lon).e_window_kill().unbind('input_tip_checking').attr('not_validate', 'true');

				} else {
					// 切换到输入设置
					if (!lon.attr('not_validate')) {					
						lon.e_input_tip({
							space : "输入经度",
							error : "格式不正确",
							rule : /^\-{0,1}\d{1,3}$|^\-{0,1}\d{1,3}.\d+$/		
						});
						lat.e_input_tip({
							space : "输入维度",
							error : "格式不正确",
							rule : /^\-{0,1}\d{1,3}$|^\-{0,1}\d{1,3}.\d+$/
						});
					}else{
						lat.add(lon).removeAttr('not_validate');
					}
					$("#show_coordinates").hide();;

				}
			}
		});
		$("#map_lon_input").e_input_tip({
			space : "",
			space_callback : function () {
				alert("请正确的设置地图坐标");
			}

		});
	})();


	// 触发图片选择
	// $(".upload_img_box").on('click', '.upload_img_btn', function(event) {
	// 	event.preventDefault();
	// 	$(this).siblings('.upload_img_input').click();
	// });

	// 公寓楼高 房间总数
	$("#hotel_building").e_input_tip({
		need : false,
		space : "0",
		error : "格式不正确",
		rule : /^\d+$/
	});
	$('.hotel_room_count').e_input_tip({
		space : "0",
		error : "格式不正确",
		rule : /^\d+$/
	});
	// 开业时间 装修时间
	(function() {
		var open_year = $('.select_yeae').eq(0),
			decoration_year = $('.select_yeae').eq(1),
			open_month = $('.select_month').eq(0),
			decoration_month = $('.select_month').eq(1);
			open_year_o = open_year.find('option'),
			decoration_year_o = decoration_year.find('option'),
			open_month_o = open_month.find('option'),
			decoration_month_o = decoration_month.find('option'),
			clone_y = open_year.clone(false, false),
			clone_m = open_month.clone(false, false);

		open_year.add(open_month).change(function(event) {
			if (decoration_year[0].selectedIndex!=0 && open_year[0].selectedIndex>=decoration_year[0].selectedIndex) {
				decoration_year[0].selectedIndex=open_year[0].selectedIndex;
				if (open_month[0].selectedIndex!=0 && decoration_month[0].selectedIndex!=0 && open_month[0].selectedIndex<decoration_month[0].selectedIndex) {
					decoration_month[0].selectedIndex = open_month[0].selectedIndex;
				};
			};
		});
		decoration_month.add(decoration_year).change(function(event) {
			var a = decoration_year[0].selectedIndex * decoration_month[0].selectedIndex * open_year[0].selectedIndex * open_month[0].selectedIndex,
				b = decoration_month[0].selectedIndex - decoration_year[0].selectedIndex * 12 + open_year[0].selectedIndex * 12 -  open_month[0].selectedIndex;
			if (a > 0 && b >0) {
				alert("装修时间不能在开业时间之后");
				decoration_year[0].selectedIndex = decoration_month[0].selectedIndex = 0;
			};
		});

	})();
	var time_select = $(".select_yeae,.select_month");
	time_select.change(function(event) {
		var p = $(this).parent(), 
			val = p.find('.select_yeae').val()+p.find('.select_month').val();
		p.find('input.hide').val(val);
		console.log(val);
	});
	$('.need_yeae,.need_month').e_input_tip({
		need_text : "必需选择"
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
		space : "请输入公寓简介(2000字以内)",
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
		space : "请输入交通位置(2000字以内)",
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

		if(f_input.length){
			set_val(f_input);
			set_val(s_input);
			set_val($('.multiple_value'));
		}

	})($)

	

	//////////////////////////////////添加房型/////////////////////////////////////////////
	// 房型名称
	$("#room_name").e_input_tip({
		space : "（如：高级大床房）",
		check : true, //失去焦点验证
		rule: function(success_callback,error_callback,val) {
			var el = $(this);

			if (!val.match(/^[\s\S]{3,}$/)) {
				error_callback("请输入三个以上的字符",el);
				return ;
			};


			$.ajax({
				url: '/Common/isOkRoom',
				dataType: 'text',
				data: {
					text:val,
					hotelId: $('[name=hotel_id]').val()
				}
			})
			.done(function(data) {
				if(data==0){
					error_callback("此房型已存在",el);
				}else{
					success_callback(el);
				}
			})
			.fail(function() {
				alert("服务器验证公寓名称失败");
			});	
		}
	})
	.autoComplete({
		url : "/common/qureyRoom",
		number:  0,
		callblack : function(data) {
			var arr = data.split("|"),
				hotel_id = $('#hotelId_input').val(),
				html = ""
				action_url = $('#action').val() == "addhotel" ? "/addhotel/selectedRoom" :"/myapartment/selectedRoom" ;

			for (var i = 0; i < arr.length; i++) {
				var r = arr[i],
					room_id = r.slice(r.indexOf(",")+1),
					name = r.slice(0,r.indexOf(","));
				html = html + '<p><a href="'+action_url+'?roomId='+ room_id +'&hotelId=' + hotel_id + '">' + name + '</a></p>';
			};
			$(this).e_window({
				position_mod: "relative", //位置模式 居中：center 相对元素 ：relative  相对窗口：absolute
				relative_mod: "bottom", //bottom right top left
				top: 5,
				left: 0,
				width: "auto",
				marginTop: 0,
				marginRight: 0,
				layer : false,
				box_id: "h_list",
				html: html
			})
		}
	});

	// 数量
	$("#room_count").e_input_tip({
			space :0,
			rule : /^\d+$/
	});

	//默认房价
	$("#default_price").e_input_tip({
			space : "0",
			need_text:"必须选择",
			rule : /^\d+$|^\d+.\d+$/
	});

	// 宜住人数
	$("#people_number").e_input_tip({
		need_text:"必须选择宜住人数"
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





	// 床型输入模块
	(function() {

		// 添加床型
		$(".bed_add").click(function(event) {
			event.preventDefault();
			var clone = $(".bed_item.hide").clone().removeClass('hide');
			$(this).before(clone);
			clone.find('.bed,.number')
			.e_input_tip({
				check : true,
				need_text : "必须选择床型"
			});
		});

		var bed_input = $("#bed_input"),
			bed_val = bed_input.val(),
			bed_items = $('.bed_item');
		// 回显
		if (bed_val) {
			var bed_arr = bed_val.split(",");
			bed_items.eq(1).remove();

			
			for (var i = 0; i < bed_arr.length; i++) {
				if(bed_arr[i]){
					var bed = bed_arr[i].split("|");
					var clone = $(".bed_item.hide").clone().removeClass('hide');
					clone.find(".bed").val(bed[0]);
					clone.find(".number").val(bed[1]);
					if (i==0) {
						clone.find('.bed_del').remove();
					};
					$(".bed_add").before(clone);
				}
			};
			bed_items = $('.bed_item');
		};

		// 床型验证
		bed_items .not(".hide").find('.bed,.number').e_input_tip({
			need_text : "必须选择"
		});

		// 删除床型
		$("#bed_box").on('click', '.bed_del', function(event) {
			event.preventDefault();
			var box = $(this).parents(".bed_item");
			box.find('.bed,.number').e_window_kill().unbind('input_tip_checking').attr('not_validate', 'true');
			box.remove();
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
		error : "最大可输入400个字符",
		space:"请输入房型描述(如：“便捷设施 入住全新体验。合理搭配 巧妙空间布局。都市中心 尊享繁华市景”。2000字以内)",
		rule: /^[\s\S]{0,400}$/
	});

	// 房型备注
	$("#room_remarks").e_input_tip({
		error : "最大可输入400个字符",
		space:"请输入房型描述",
		need: false,
		rule: /^[\s\S]{0,400}$/
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
	

	///////////////////////////////////////上传图片 回显/////////////////////////
	$(".upload_img").e_img_siz("",true);

	// 绑定验证
	$(".upload_img_info").e_input_tip({
		space:"请输入图片描述",
		need:false
	});
	$(".upload_img_type").e_input_tip({
		need_text:"必须选择"
	});

	function upload_img(els) {
		els.each(function(index, el) {
			var el = $(el),
				box = el.parents(".from_path").find('.img_show_box'),
				info_box_id = 'info_box' + new Date().getTime();

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

					info_box = $(upload_tip).appendTo(box).attr('id', info_box_id );
					info_box.find('p').html(filename+"<br><br>正在上传中...");

					return {
						roomid: this.attr('room_id')
					}
				},
				onComplete: function(filename, response) {
					// alert(JSON.stringify(response));
					for (var i = 0; i < response.length; i++) {
						var img = response[i];
						var a = $(html).insertBefore('#'+info_box_id);

						a.find('img').attr('src', img.tURL).attr('oURL',img.oURL).e_img_siz("",true);
						if (img.PID) {
							a.attr('pid', img.PID);
							a.find('select').html($("#img_type_sel").html());

							// 绑定验证
							a.find('.upload_img_info').e_input_tip({
								space:"请输入图片描述",
								need:false
							});
							a.find(".upload_img_type").e_input_tip({
								need_text:"必须选择"
							});
						}else {
							a.attr('pid', "error");
							a.find('.img_set').addClass('col_red').html(img.Message);
						}
						
					};
					$('#'+info_box_id).remove();
				},
				onError: function(filename){
					$('#'+info_box_id).find('p').html(filename+"<br><br>正在上传失败...");
				}
			});
		});
	}

	upload_img($(".upload_img_input"));

	// 设置图片描述
	$("#add_img").on('focusout',".upload_img_info", function(event) {
		var el = $(this),
			pid = el.parents(".upload_img_box").attr('pid'),
			v = $(this).val(),
			ajax_load = "";


		if (v && v!= el.attr('last_value')) {
			if(v == "请输入图片描述"){
				v="";
			}
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
				}else {
					el.attr('last_value', v);
				}
			})
			.fail(function(data) {
				alert("描述提交错误！错误代码：" + data.status + ","+data.statusText + "。");
			});
		};
	});

	// 设置图片类型
	$("#add_img").on('change',".upload_img_type", function(event) {
		var el = $(this),
			pid = el.parents(".upload_img_box").attr('pid'),
			v = el.val();

		if (v) {
			$(this).prop('disabled',true);

			//提交类型
			$.ajax({
				url: '/ImageProperty/ImageType',
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
			pid = box.attr('pid'),
			t_url = box.find('.upload_img').attr('src'),
			o_url = box.find('.upload_img').attr('oURL'),
			data = {
				PID : pid=="error"?0:pid,
				text1 : t_url,
				text2 : o_url
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
				box.find('.upload_img_info,.upload_img_type').e_window_kill().unbind('input_tip_checking').attr('not_validate', 'true');
				box.remove();
			}
		})
		.fail(function(data) {
			alert("删除图片错误！错误代码："+data.status+","+data.statusText+"。");
		});
		
	});

	//////////////////////////////////设置促销规则////////////////////////////////////////
    // 酒店切换
    $("#hotel_switch_my_drr").change(function(event) {
        console.log(event);
        window.location.href="/DrrRule/MyDrr?id="+$(this).find("option:selected").val();
    });




	// 验证表单
	$("#drr_name").e_input_tip({
		space : "促销价格的名称",
		check : true, //失去焦点验证
		rule: function(success_callback,error_callback,val) {
			var el = $(this);

			if (!val.match(/^[\s\S]{3,}$/)) {
				error_callback("请输入三个以上的字符",el);
				return ;
			};


			$.ajax({
				url: '/DrrRule/IsOk/',
				dataType: 'text',
				data: {
					text:val,
					id : $("#hotel_id").val()
				}
			})
			.done(function(data) {
				if(data==0){
					error_callback("此促销价格的名称已存在",el);
				}else{
					success_callback(el);
				}
			})
			.fail(function(data) {
				alert("服务器验证公寓销价名称失败");
			});	
		}
	});

	var drr_modes = $('#drr_modes_hide').find('.input_line'),
		drr_mode = $('.drr_mode');
	drr_mode.find('input').e_input_tip();
	// 促销规则切换
	$(".drr_modes").change(function(event) {
		drr_mode.find('input').e_window_kill().attr('not_validate', 'true');
		drr_mode.html(drr_modes.eq(this.selectedIndex).clone(false, false));
		drr_mode.find('input').e_input_tip();


	});

	//////////////////////////////////设置礼包////////////////////////////////////////
    // 酒店切换
    $("#hotel_switch_gift").change(function(event) {
        console.log(event);
        window.location.href="/Gift/MyGift?id="+$(this).find("option:selected").val();
    });

    $('#GiftContent').e_input_tip();
    
	// 礼包和促销同时使用此验证
	$('#rooms').e_input_tip({
		space_callback : function() {
			alert("请选择适用房型");
		}
	});
 
	//////////////////////////////////设置担保////////////////////////////////////////
    // 酒店切换
    $("#hotel_switch_my_guarantee").change(function(event) {
        console.log(event);
        window.location.href="/Guarantee/MyGuarantee?id="+$(this).find("option:selected").val();
    });

    $('.g_ru_change').click(function(event) {
    	if($(this).index(".g_ru_change")==1){
    		$("#notify_time").e_input_tip().removeAttr('not_validate');
    	}else{
    		$("#notify_time").e_window_kill().unbind('input_tip_checking').attr('not_validate', 'true');
    	}
    });

    // 提交担保
	$(".MyGuarantee_btn").click(function(event) {
		event.preventDefault();
		var el = $(".g_ru_change:checked"),
			val = $(".security_costs:checked").next().text()+"|";

		if (el.index(".g_ru_change") ==1) {
			val = val+"允许变更/取消,需在最早到店时间之前"+$('#notify_time').val()+"小时通知";
		}else {
			val = val + "不许变更/取消";
		}
		$(".MyGuarantee_Description").val(val);
		$(this).parents("form").submit();
	});

	//////////////////////////////////房价房态////////////////////////////////////////
    // 酒店切换
    $("#hotel_switch_my_price").change(function(event) {
        console.log(event);
        window.location.href=  setUrlParam("id",$(this).find("option:selected").val(),location.href);
    });

	// 切换日期
    (function() {
    	if (!$("#date_load").length) {
    		return;
    	};
    	var start = {
	        elem: '#date_load',
	        min: laydate.now(),
	        istoday: true,
	        choose: function (datas) {
	        	console.log(datas);
	            location.href = setUrlParam("startDate",datas,location.href);
	        }
	    };
	    laydate(start);
    })();

    // 弹窗模块
    (function() {
    	var el_ing = "",
    		send_data = {};

	    $(".item_pr").click(function(event) {
	    	if (el_ing) {
	    		el_ing.e_window_kill();
	    	};

	    	var el = $(this),
	    		html = $("#pr_set_box").clone(false, false).removeClass('hide');

	    	html.find('.date_start').val(el.attr('date'));
	    	html.find('.date_end').val(el.attr('date'));
	    	html.find('.only_integer').val(el.text());
	    	var status = html.find('.status_val');
	    	if (status.length) {
		    	status.val(el.text().split("/")[1].replace(/(^\s*)|(\s*$)/g, ""));
		    	if (el.is(".grey ")) {
		    		html.find("[name=r_stats]")[1].checked = true ;
		    	}else {
		    		html.find("[name=r_stats]")[0].checked = true ;
		    	}
	    	};


	    	send_data.id = el.attr("Hotel_id");
	    	send_data.roomId = el.attr("roomid");

	    	el_ing = el.e_window({
				position_mod: "relative", //位置模式 居中：center 相对元素 ：relative  相对窗口：absolute
				relative_mod: "bottom", //bottom right top left
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

			var end_node = $('#set_pr_box .date_end')[0],
				start_node = $('#set_pr_box .date_start')[0];

	    	function set_date() {
		        var start = {
		            elem: '#set_pr_box .date_start',
		            min: laydate.now(), //设定最小日期为当前日期
		            istoday: false,
		            choose: function (datas) {
		                //end.min = datas; //开始日选好后，重置结束日的最小日期
		                //end.start = datas //将结束日的初始值设定为开始日
						if (DateTool.comparisonDate(start_node.value, end_node.value) > 0) {
							end_node.value = start_node.value;
						};
						if (DateTool.comparisonDate(start_node.value, end_node.value) < -365*24*60*60*1000 ) {
							var d = DateTool.timeToStrimg(Date.parse(start_node.value)+ 365*24*60*60*1000);
							end_node.value = d;
							alert("单次设置的限制范围为365天!\n已将结束日期设为"+ d);

						};
		            }
		        };
		        var end = {
		            elem: '#set_pr_box .date_end',
		            min: laydate.now(),
		            istoday: false,
		            choose: function (datas) {
		                //start.max = datas; //结束日选好后，充值开始日的最大日期
						if (DateTool.comparisonDate(start_node.value, end_node.value) > 0) {
							start_node.value = end_node.value;
						};
						if (DateTool.comparisonDate(start_node.value, end_node.value) < -365*24*60*60*1000 ) {
							var d = DateTool.timeToStrimg(Date.parse(start_node.value)+ 365*24*60*60*1000);
							end_node.value = d;
							alert("单次设置的限制范围为365天!\n已将结束日期设为"+ d);
						};
		            }
		        };
		        laydate(start);
		        laydate(end);
	    	}
	    	setTimeout(set_date, 100);
	    });


	   // 修改房价按钮
	    $("body").on('click', '.set_pr_btn', function(event) {
	    	event.preventDefault();
	    	var box = $(this).parents(".set_box");
	    	send_data.startDate = box.find('.date_start').val();
	    	send_data.EndDate = box.find('.date_end').val();
	    	send_data.value = box.find('.only_integer').val();
	    	console.log(send_data);

	    	// todo 验证

	    	$.ajax({
	    		url: '/price/uPrice/',
	    		type: 'GET',
	    		data: send_data,
	    	})
	    	.done(function(data) {
	    		console.log(data);
	    		if (data == 1) {
	    			location.reload();
	    		}else {
	    			alert("修改失败！");
	    		}
	    	})
	    	.fail(function() {
	    		alert("服务器错误！");
	    		console.log("服务器错误！");
	    	});
	    	
	    });

	    // 修改房态按钮
		$("body").on('click', '.set_status_btn', function(event) {
	    	event.preventDefault();
	    	var box = $(this).parents(".set_box");
	    	send_data.startDate = box.find('.date_start').val();
	    	send_data.EndDate = box.find('.date_end').val();
	    	send_data.CanSell = box.find('.only_integer').val();
	    	send_data.status = box.find('[name=r_stats]:checked').val();
	    	console.log(send_data);

	    	// todo 验证

	    	$.ajax({
	    		url: '/RStatus/uStatus/',
	    		type: 'GET',
	    		data: send_data,
	    	})
	    	.done(function(data) {
	    		console.log(data);
	    		if (data == 1) {
	    			location.reload();
	    		}else {
	    			alert("修改失败！");
	    		}
	    	})
	    	.fail(function() {
	    		alert("服务器错误！");
	    		console.log("服务器错误！");
	    	});
	    	
	    });
    })();


    ///////////////////////////订单管理/////////////////////////////////////
    // 操作 弹窗模块
    (function() {
    	var el_ing = "",
    		send_data = {};

	    $(".operation_order").click(function(event) {
	    	event.preventDefault();

	    	if (el_ing) {
	    		el_ing.e_window_kill();
	    	};

	    	var el = $(this),
	    		html = $(".set_box").clone(false, false).removeClass('hide');

	    	html.find('.order_id').val(el.attr('order_id'));

	    	el_ing = el.e_window({
				position_mod: "relative", //位置模式 居中：center 相对元素 ：relative  相对窗口：absolute
				relative_mod: "bottom", //bottom right top left
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

	// 确定按钮
	$("body").on('click', '.modify_order_status_btn', function(event) {
		event.preventDefault();
		var form = $(this).parents("form"),
			data = form.serialize();
		console.log(data);
		form.submit();
	});

    // 弹窗的交互
    $("body").on('click', '.confirmation_order', function(event) {
    	var el = $(this),
    		box = el.parents(".set_box"),
    		rejection_box = box.find('.rejection_box');

    	if (box.find('.confirmation_order').index(el) == 1 ) {
    		rejection_box.show();
    	}else{
    		rejection_box.hide();	
    	}
    });
    $("body").on('click', '.rejection_radio', function(event) {
    	var el = $(this),
    		box = el.parents(".rejection_box"),
    		rejection_text = box.find('.rejection_text');

    	if (box.find('.rejection_radio').index(el) == 2 ) {
    		rejection_text.show();
    	}else{
    		rejection_text.hide();	
    	}
    });

    ////////////////////////////////////////////////结算信息/////////////////////////////////////////////////////
    $('#bill_hotel').change(function(event) {
        window.location.href="/Bill/QureyBill?hotelId="+$(this).find("option:selected").val();
    });
    $('#bill_btn').click(function(event) {
    	event.preventDefault();
        window.location.href="/Bill/QureyBill?hotelId="+$("#bill_hotel").find("option:selected").val() + 
        "&startTime=" + $('.date_start').val()+ "&endTime=" + $('.date_end').val();

    });

    ////////////////////////////////////////////////评论管理/////////////////////////////////////////////////////
    $('#comment_hotel').change(function(event) {
        window.location.href="/Comment/QueryComment?hotelId="+$(this).find("option:selected").val();
    });
    $('#comment_btn').click(function(event) {
    	event.preventDefault();
        window.location.href="/Comment/QueryComment?hotelId="+$("#comment_hotel").find("option:selected").val() + 
        "&startTime=" + $('.date_start').val()+ "&endTime=" + $('.date_end').val() + "&IsReply=" + $('#IsReply').val();

    });

    $('.show_comment_reply').click(function(event) {
    	event.preventDefault();
    	var el = $(this);
    	if (el.text()=="回复") {
    		el.parents("tr").find('.comment_p').show(800);
    		el.text('收起回复');
    	}else {
    		el.parents("tr").find('.comment_p').hide(800);
    		el.text('回复');
    	}
    });

    $('.comment_submit_btn').click(function(event) {
    	event.preventDefault();
    	$(this).parents("form").submit();
    });

	////////////////////////////////////////////////用户管理/////////////////////////////////////////////////////
	$('#input_user_name').e_input_tip({
		need: true,
		space: "用户姓名(18个字符内)",
		rule: /^[\s\S]{1,18}$/
	})

	$('#input_user_password').e_input_tip({
		need: true,
		space: "",
		rule: /^[\s\S]{6,18}$/,
		space_callback: function(need_text, el) {
			el.hide().next().show()
			.e_window({
				top: 5,
				width: "auto",
				html: "<div class='red_tip_box'>" + need_text + "</div>"
			});
		},
		success_callback: function(el) {
			el.e_window_kill();
			el.next().e_window_kill();
		}
	})

	$('#input_user_password_checkin').e_input_tip({
		need: true,
		space: "",
		rule: function (success_callback,error_callback,val) {
			if(!/^[\s\S]{6,18}$/.exec(val)){
				error_callback();
				return false;
			}

			if (val != $('#input_user_password').val()) {
				error_callback("两次输入密码不一致");
				return false
			};

			return true;
		},
		space_callback: function(need_text, el) {
			el.hide().next().show()
			.e_window({
				top: 5,
				width: "auto",
				html: "<div class='red_tip_box'>" + need_text + "</div>"
			});
		},
		success_callback: function(el) {
			el.e_window_kill();
			el.next().e_window_kill();
		}
	})

	$('.show_password_input').click(function(event) {
		$(this).hide();
		$(this).prev().show().focus();
	});

	// 密码回显
	if($('#input_user_password').val()){
		$('.show_password_input').hide();
		$('#input_user_password').add('#input_user_password_checkin').show();
	}


	$('#user_phone').e_input_tip({
		need : true,
		space : "11位手机号码",
		rule : /^\d{11}$/
	});
    ////////////////////////////////////////////////投诉和建议/////////////////////////////////////////////////////

    $('#complaint_contact,#complaint_content').e_input_tip();



	/////////////////////////////////////////////////////////////////功能控件////////////////////////////////////////////////////////////////
	// 操作失败提示
	$(function() {
		if($('#word_failure').val()===0){
			alert("操作失败！");
		}
	});

 	// 房型回显
	(function($){
		function set_val (input) {
			var data_arr = input.val().split(","),
				multiple = input.parent().find('.multiple');

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

		var rooms = $("#rooms"),
			rooms_text = $("#rooms_text");

		if(rooms.length){
			set_val(rooms);
			set_val_b(rooms_text);
		}

	})($)

	//多选值处理
	$(".multiple").change(function(event) {
		var checkbox_box = $(this).parents(".checkbox_box"),
			input = $(this).parents(".input_line").prev(".hide"),
			vals = "",
			texts = "";
		if(!input.length) {
			input = $(this).parents(".input_line").find(".hide").eq(0);
			var input_text = input.next(".hide");
		}
		checkbox_box.find('.multiple').each(function(index, el) {
			if ($(this).attr('checked')) {
				vals += ($(this).val()+",");
				texts += ($(this).next().text()+"、");
			};
		});
		vals = vals.slice(0, -1);
		texts = texts.slice(0, -1);
		input.val(vals);
		console.log(input.val());

		if (input_text) {
			input_text.val(texts);
			console.log(input_text.val());
		};
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

	// 多选模块升级
	(function($) {
		$(".multiple2").change(function(event) {
			var checkbox_box = $(this).parents(".checkbox_box"),
				input_values = checkbox_box.find('.multiple_values'),
				input_texts = checkbox_box.find('.multiple_texts'),
				vals = "",
				texts = "";
			checkbox_box.find('.multiple2').each(function(index, el) {
				if ($(this).attr('checked')) {
					vals += ($(this).val()+",");
					texts += ($(this).next().text()+",");
				};
			});
			vals = vals.slice(0, -1);
			texts = texts.slice(0, -1);
			input_values.val(vals);
			input_texts.val(texts);
		});
		// 全选
		$(".all_set").click(function(event) {
			event.preventDefault();
			$(this).parents(".checkbox_box").find('.multiple2').each(function(index, el) {
				$(this).attr('checked',"").change();
			});
		});
		// 反选
		$(".reverse_set").click(function(event) {
			event.preventDefault();
			$(this).parents(".checkbox_box").find('.multiple2').each(function(index, el) {
				if($(this).attr('checked')){
					$(this).removeAttr('checked');
				}else {
					$(this).attr('checked',"");
				}
				$(this).change();
			});
		});

		// 回显
		function setInputValues (vals_input,texts_input) {
			var checkbox_box = vals_input.parents(".checkbox_box"),
				data_arr = vals_input.val().split(","),
				multiple = checkbox_box.find('.multiple2');
			for (var i = 0; i < data_arr.length; i++) {
				var val = data_arr[i];
				multiple.filter('[value='+val+']').attr('checked', 'true');
			};
			// if(texts_input.length){
			// 	var data_arr = texts_input.val().split(",");
			// 	for (var i = 0; i < data_arr.length; i++) {
			// 		var val = data_arr[i];
			// 		label.filter(function(){
			// 		   return $(this).text() == val;
			// 		}).prev().attr('checked', 'true');
			// 	};
			// }	
		}

		$('.multiple_values').each(function(index, el) {
			setInputValues($(this));
		});

	})($)

	// class only_number 只能输入数字
	$("body").on("keypress",".only_integer",function(event) {
		var key_code = event.keyCode == 0 ? event.which : event.keyCode;
		console.log(key_code);
		if (key_code>=48 && key_code<=59|| key_code == 8) {

		}else {
			event.preventDefault();
		}


	}).on('change', '.only_integer', function(event) {
		var el = $(this);
		el.val(el.val().replace(/\D/g,""));
	});


	// class only_float 只能输入浮点数
	$("body").on("keypress",".only_float",function(event) {
		var key_code = event.keyCode == 0 ? event.which : event.keyCode;
		console.log(key_code);
		if (key_code>=48 && key_code<=59 || key_code == 46) {

		}else {
			event.preventDefault();
		}


	}).on('change', '.only_float', function(event) {
		var el = $(this);
		el.val(el.val().replace(/[^\d\.]/g,""));
	});


	// 验证表单
	$(".checking_btn").click(function(event) {
		event.preventDefault();
		var el = $(this),
			status = 0;

		var err_el = $('[rules_error]');

		// 图片数量验证
		$(".room_img_item").each(function(index, el) {
			if($(this).find('.img_set').not('.col_red').length<5){
				status = 2;
			}
		});

		if (status == 2) {
			alert("每个房型图片不能少于5张。")
			return
		}
		$("form").submit();

		// 	$(".room_img_item").each(function(index, el) {
		// 		if($(this).find('.img_set').length<5){
		// 			status = 2;
		// 		}
		// 	});
		// 	$("#bed_input").each(function() {
		// 		if(!$(this).val()){
		// 			status = 3;
		// 		}
		// 	});

		// 		document.forms[0].submit();
		// 	}else {
		// 		if(status == 1){
		// 			var Message = "填写的信息没有通过验证，请检查。";
		// 		};
		// 		if(status == 2){
		// 			var Message = "每个房型图片不能少于5张。";
		// 		};
		// 		if(status == 3) {
		// 			var Message = "必须设置床型";
		// 		};
		// 		el.e_window({
		// 			relative_mod : "right",
		// 			left : 30,
		// 			width: "auto",
		// 			html: "<div class='red_tip_box'>"+ Message +"</div>"
		// 		});
		// 		setTimeout(function() {
		// 			el.e_window_kill();
		// 		}, 5000);
		// 	}
		// })
		
		// var input = $(this)
		// 			.parents(".box_a")
		// 			.find('input[type=text],select[name],textarea[name],.select_yeae,.select_month')
		// 			.not("#hotel_name");
		// 			// .trigger("input_tip_checking");

		// setTimeout(function() {
		// 		
		// 	};				
		// }, 200);
	});


	// 设置url参数
	function setUrlParam(para_name,para_value,url)
	{
	        var strNewUrl=new String();
	        var strUrl=url;
	        //alert(strUrl);
	        if(strUrl.indexOf("?")!=-1)
	        {
	            strUrl=strUrl.substr(strUrl.indexOf("?")+1);
	            //alert(strUrl);
	            if(strUrl.toLowerCase().indexOf(para_name.toLowerCase())==-1)
	            {
	                strNewUrl=url+"&"+para_name+"="+para_value;
	                return strNewUrl;
	            }else
	            {
	                var aParam=strUrl.split("&");
	                //alert(aParam.length);
	                for(var i=0;i<aParam.length;i++)
	                {
	                    if(aParam[i].substr(0,aParam[i].indexOf("=")).toLowerCase()==para_name.toLowerCase())
	                    {
	                       aParam[i]= aParam[i].substr(0,aParam[i].indexOf("="))+"="+para_value;
	                    }
	                }
	               
	               strNewUrl=url.substr(0,url.indexOf("?")+1)+aParam.join("&");
	              // alert(strNewUrl);
	               return strNewUrl;
	           }
	            
	        }else
	        {
	            strUrl+="?"+para_name+"="+para_value;
	            //alert(strUrl);
	            return strUrl
	        }
	}



    var DateTool = {
		// 比较xxxx-xx-xx格式的日期
	    comparisonDate : function (start,end) {
	        if (!start||!end) {
	            return;
	        };
	        var start_arr = start.split("-"),
	            end_arr = end.split("-"),
	            startTime = new Date(),
	            endTime = new Date();
	        startTime.setFullYear(start_arr[0]);
	        startTime.setMonth(start_arr[1]-1);
	        startTime.setDate(start_arr[2]);
	        startTime.setHours(0);
	        startTime.setMinutes(0);
	        startTime.setSeconds(0);
	        startTime = startTime.setMilliseconds(0);
	        endTime.getTime();
	        endTime.setFullYear(end_arr[0]);
	        endTime.setMonth(end_arr[1]-1);
	        endTime.setDate(end_arr[2]);
	        endTime.setHours(0);
	        endTime.setMinutes(0);
	        endTime.setSeconds(0);
	        endTime.setMilliseconds(0);
	        endTime = endTime.getTime();
	        return startTime-endTime;
	    },

	    // Time转换为XXXX-XX-XX
	    timeToStrimg : function(time) {
	    	var date = new Date(time)
	    	return date.getFullYear()+"-"+ (date.getMonth()+1) + "-" + date.getDate();
	    }
    };

})(jQuery);


