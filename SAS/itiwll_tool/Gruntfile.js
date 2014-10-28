module.exports = function(grunt) {


	var date = new Date(),
		date_str = "/*" + date.getFullYear() + '年' + (date.getMonth() + 1) + '月' + date.getDate() + '日' + date.toLocaleTimeString() + "*/",
		js_files = [
				"src/script/plug-in/input_tip.js",
				"src/script/plug-in/e_img_siz.js",
				"src/script/plug-in/tab_switch.js",
				"src/script/plug-in/e_window.js",
				"src/script/plug-in/jquery.ajaxfileupload.js",
				"src/script/plug-in/autoComplete.js",
				"src/script/main.js"
			],
		css_files = {
			'../Content/public/style/main.css' : [
				"src/style/main.css",
				"src/style/header_and_footer.css",
				"src/style/base.css"
			]
		};


	grunt.initConfig({
		// js操作
		uglify: {
			debug: {
				options: {
					banner: date_str,
					mangle: false,
					compress: false,
					beautify: true,
					sourceMap: true
				},
				files: {
					'../Content/public/script/main.js' : js_files
				}
			},
			dev_js: {
				options: {
					banner: date_str,
					mangle: true,
					compress: true,
					beautify: false,
					sourceMap: false
				},
				files: js_files
			}
		},

		cssmin : {
			debug: {
				options:{
					banner: date_str,
				},
				files:css_files
			}
		},

		// 监视文件变化刷新浏览器
		watch: {
			config: {
				files: ["./*"]
			},
			css: {
				files: ["src/style/*.css"],
				tasks: ["cssmin:debug"],
				options: {
					//刷新浏览器
					livereload: true,
					dateFormat: function(time) {
						grunt.log.writeln(time + ":" + "修改css，拷贝");
					}
				}
			},
			html: {
				files: ["../Views/**/*"],
				// tasks: ["copy:html"],
				options: {
					//刷新浏览器
					livereload: true,
					dateFormat: function(time) {
						grunt.log.writeln(time + ":" + "修改html");
					}
				}
			},
			js: {
				files: js_files,
				tasks: ["uglify:debug"],
				options: {
					//刷新浏览器
					livereload: true
				}
			}
		}
	});

	grunt.loadNpmTasks('grunt-contrib-cssmin');
	grunt.loadNpmTasks('grunt-contrib-uglify');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-contrib-watch');

	grunt.registerTask('default', ['watch']);
	grunt.registerTask('dev', ['cssmin:combine','uglify:dev_js']);
};