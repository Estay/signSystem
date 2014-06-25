/********************************************************
 ** jQuery itiwll_input_tip
 ** 20140623
 **
 **********************************************************/

// 输入框提示文字
$(".tip_input").e_input_tip();

//录入设施与服务 值处理
$(".multiple").change(function(event) {
	var input = $(this).parents(".input_line").prev(".hide"),
		vals = input.val(),
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
		console.log("data");
	})
	.fail(function() {
		console.log("error");
	})
	.always(function() {
		console.log("complete");
	});
	
});