/********************************************************
 ** @itiwll
 ** 20140623
 **
 **********************************************************/
(function($) {
	var estay_sas = {};

	// 输入框提示文字
	$(".tip_input").e_input_tip();

	//电话值处理
	$("#phone_area_code,#hotel_phonehotel_phone").keyup(function(event) {
		$("#phone").val($("#phone_area_code").val()+"-"+$("#hotel_phonehotel_phone").val());
	});

	//多选值处理
	$(".multiple").change(function(event) {
		var input = $(this).parents(".input_line").prev(".hide");
		input = input.length?input : $(this).parents(".input_line").find(".hide");
		var	vals = input.val(),
			val = $(this).val();
		if (vals.indexOf(val) > -1) {
			vals =  vals.replace(RegExp("^" + val + ",|," + val + "|^" + val + "$", "ig"), '');
		}else{
			vals = vals ? vals + "," + val : val;
		};
		input.val(vals);
	});

	// 设置省份
	$("#hotel_province").change(function(event) {
		// 地图同步
		map.centerAndZoom($(this).find(':selected').text());
		$.ajax({
			url: '/help/location.ashx',
			type: 'GET',
			dataType: 'json',
			data : {
				type : "city",
				value: $(this).val()
			}
		})
		.done(function(data) {
			$("#h_city").children().slice(1).remove();
			$("#h_administrative_region").children().slice(1).remove();
			$("#h_business_zone").children().slice(1).remove();
			for (var i = 0; i < data.length; i++) {
				var city = data[i];
				var option = '<option value="'+city.id+'">'+city.name+'</option>';
				$("#h_city").append(option);
			};
		})
		.fail(function() {
		})
		.always(function() {
		});
	});

	// 设置城市
	$("#h_city").change(function(event) {
		//地图同步
		map.centerAndZoom($(this).find(':selected').text());

		// 加载行政区数据
		$.ajax({
			url: '/help/location.ashx',
			type: 'GET',
			dataType: 'json',
			data : {
				type : "region",
				value: $(this).val()
			}
		})
		.done(function(data) {
			$("#h_administrative_region").children().slice(1).remove();
			for (var i = 0; i < data.length; i++) {
				var city = data[i];
				var option = '<option value="'+city.id+'">'+city.name+'</option>';
				$("#h_administrative_region").append(option);
			};
		})
		.fail(function() {
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
				value: $(this).val()
			}
		})
		.done(function(data) {
			$("#h_business_zone").children().slice(1).remove();
			for (var i = 0; i < data.length; i++) {
				var city = data[i];
				var option = '<option value="'+city.id+'">'+city.name+'</option>';
				$("#h_business_zone").append(option);
			};
		})
		.fail(function() {
		})
		.always(function() {
		});
	});
	$("#h_administrative_region,#h_business_zone").change(function(event) {
		map.centerAndZoom($(this).find(':selected').text());
	});

	// 地图输入方式切换
	$("#location_box").e_tab_switch();
	$("#map_lon").keyup(function(event) {
		$("#map_lon_input").val($(this).val());
		$("#map_lon_text").text($(this).val());
	});
	$("#map_lat").keyup(function(event) {
		$("#map_lat_input").val($(this).val());
		$("#map_lat_text").text($(this).val());
	});

	// 触发图片选择
	// $(".upload_img_box").on('click', '.upload_img_btn', function(event) {
	// 	event.preventDefault();
	// 	$(this).siblings('.upload_img_input').click();
	// });

	//上传图片
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

			var upload_tip =  '<div class="upload_img_box upload_info_box">';
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
				url: '/help/ImageDes.ashx',
				type: 'GET',
				data: {
					PID: pid,
					Description: v}
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
		var pid = $(this).parents(".upload_img_box").data('pid'),
			v = $(this).val();

		if (v) {

			//提交类型
			$.ajax({
				url: '/help/ImageDes.ashx',
				type: 'GET',
				data: {
					PID: pid,
					text : v
				}
			})
			.done(function(data) {
				if (data==0) {
					alert("设置图片类型失败");
					$(this)[0].selectedIndex=0;
				};
			})
			.fail(function(data) {
				alert("提交图片类型错误！错误代码："+data.status+","+data.statusText+"。");
			});
		};
	});

	// 删除图片按钮
	$("#add_img").on('click', '.img_del', function(event) {
		event.preventDefault();
		var box = $(this).parents(".upload_img_box"),
			pid = box.data('pid'),
			url = box.find('.upload_img').attr('src'),
			data = {
				PID : pid=="error"?0:pid,
				URL : url
			};

		//删除图片请求
		$.ajax({
			url: '/help/ImageDel.ashx',
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
	

})(jQuery);


