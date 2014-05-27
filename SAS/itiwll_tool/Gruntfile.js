module.exports = function(grunt) {


	var date = new Date(),
		date_str = "/*" + date.getFullYear() + '年' + (date.getMonth() + 1) + '月' + date.getDate() + '日' + date.toLocaleTimeString() + "*/",
		js_files = {
			'../Content/public/script/main.js': [
				"src/script/main.js"
			]
		},
		css_files = {
			'../Content/public/style/main.css' : [
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
				files: js_files
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
				files: ["script/*.js"],
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