var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var uglify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var gutil = require('gulp-util');
var ngAnnotate = require('gulp-ng-annotate')

gulp.task('browserify', function() {
    return browserify('./Scripts/index.js')
        .bundle()
        .pipe(source('main.js'))
        .pipe(buffer())
        .pipe(sourcemaps.init({loadMaps: true}))
        .pipe(uglify())
        .pipe(sourcemaps.write('./'))
        .on('error', gutil.log)
        .pipe(gulp.dest('./Scripts/bundles/'));
});

gulp.task('watch', function() {
    gulp.watch(['./Scripts/**/*.js', '!./Scripts/.idea/*', '!./Scripts/bundles/*'], ['browserify'])
});

gulp.task('default', ['watch']);

//gulp.task('browserify', function() {
//    return browserify('./Scripts/index.js')
//        .bundle()
//        .pipe(source('main.js'))
//        .pipe(buffer())
//        .pipe(sourcemaps.init({loadMaps: true}))
//        .pipe(ngAnnotate())
//        .pipe(uglify())
//        .on('error', gutil.log)
//        .pipe(sourcemaps.write('./'))
//        .pipe(gulp.dest('./Scripts/bundles/'));
//});