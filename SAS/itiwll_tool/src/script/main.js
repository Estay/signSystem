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
				label = box.find('label');

			var html = '';
				html +=			'<div class="upload_img_box">';
				html +=				'<div class="img_box"><img /></div>';
				html +=				'<p class="img_set">';
				html +=					'<input type="text" class="upload_img_info">';
				html +=					'<select name="img_type">';
				html +=						'<option value="大堂">大堂</option>';
				html +=					'</select>';
				html +=				'</p>';
				html +=				'<p class="img_del"><a href="">删除</a></p>';
				html +=			'</div>';


			el.AjaxFileUpload({
				action: "/help/FileHandle.ashx",
				onSubmit: function() {



					label.find('span').text('上传中...');
					return {
						roomid: this.attr('room_id')
					}
				},
				onComplete: function(file, response) {
					// alert(JSON.stringify(response));
					var data = response.split("&");
					$(html).appendTo(box).find('img').attr('src', data[0]).data('pid', data[1]);
				}
			});
		});
	}

	upload_img($(".upload_img_input"));

	// 上传图片说明
	function upload_imginfo (els) {
		els.each(function(index, el) {
			var el = $(el),
				pid = el.attr('pid');
			el.focusout(function(event) {
				$.ajax({
					url: '/path/to/file',
					type: 'default GET (Other values: POST)',
					dataType: 'default: Intelligent Guess (Other values: xml, json, script, or html)',
					data: {param1: 'value1'},
				})
				.done(function() {
					console.log("success");
				})
				.fail(function() {
					console.log("error");
				})
				.always(function() {
					console.log("complete");
				});
				
			});
		});
	}

})(jQuery);


