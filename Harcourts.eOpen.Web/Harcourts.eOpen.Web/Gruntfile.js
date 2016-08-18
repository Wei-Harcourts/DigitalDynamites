/**
 * Configure Grunt - http://benfrain.com/lightning-fast-sass-compiling-with-libsass-node-sass-and-grunt-sass/
 */

module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        watch: {
            sass: {
                files: [
                    'Scss/{,*/}*.{scss,sass}'
                ],
                tasks: ['sass:dist']
            }
        },
        sass: {
            dist: {
                options: {
                    includePaths: [
                        require('node-bourbon').includePaths,
                        "node_modules/font-awesome/scss/"
                    ],
                    check: false,
                    precision: 10,
                    outputStyle: "compressed"
                },
                files: {
                    'Content/Site.css': 'Scss/main.scss'
                }
            }
        },
        concat: {
            thirdparty: {
                files: [{
                    dest: 'Content/thirdparty.min.js',
                    src: [
                        'node_modules/fullpage.js/jquery.fullPage.js'
                    ]
                }]
            }
        }
    });
    

    grunt.registerTask('default', [
        'sass',
        'concat:thirdparty'
    ]);
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');
};
