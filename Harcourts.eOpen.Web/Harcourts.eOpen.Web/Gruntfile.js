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
//        useminPrepare: {
//            html: {
//                src: ['Views/Dummy/_Layout.html']
//            },
//            options: {
//                dest: './'
//            }
//        },
//        usemin: {
//            html: {
//                src: ['Views/Dummy/_Layout.html']
//            }
//        },
//        filerev: {
//            options: {
//                algorithm: 'md5',
//                length: 8
//            },
//            files: {
//                src: [
//                    'Content/Site.min.js',
//                    'Content/thirdparty.min.js'
//                ],
//                dest: 'Content'
//            }
//        },
        concat: {
            thirdparty: {
                files: [
                    {
                        dest: 'Content/thirdparty.min.js',
                        src: [
                            'node_modules/fullpage.js/jquery.fullPage.js'
                        ]
                    }
                ]
            }
        }
    });
    

    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
//    grunt.loadNpmTasks('grunt-usemin');
    grunt.loadNpmTasks('grunt-contrib-concat');
//    grunt.loadNpmTasks('grunt-contrib-uglify');
//    grunt.loadNpmTasks('grunt-filerev');

    grunt.registerTask('default', [
        'sass',
        'concat'
    ]);
};
