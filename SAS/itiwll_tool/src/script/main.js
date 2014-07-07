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
	$(".upload_img_box").on('click', '.upload_img_btn', function(event) {
		event.preventDefault();
		$(this).siblings('.upload_img_input').click();
	});

	//上传图片
	$(".upload_img_input").AjaxFileUpload({
		action: "/help/FileHandle.ashx",
		onSubmit: function(filename) {
			console.log($(this));
			console.log(filename);
			$(this).siblings('.upload_img_btn').text("上传中...");
			// return {
			// 	room_id : $(this).attr('room_id')
			// };
		},
		onComplete: function(filename, response) {
			$(this).parent(".upload_img_box").append(
				$("<img />").attr("src", filename).attr("width", 200)
			);
		}
	});
})(jQuery);


