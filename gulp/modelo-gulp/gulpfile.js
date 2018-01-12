var gulp = require('gulp'),
connect = require('gulp-connect-multi')();
  
gulp.task('arquivos', function(){
  gulp.src(['./src/**/**/*.*'])
  .pipe(gulp.dest('./build'))
  .pipe(connect.reload())
});
  
gulp.task('servidor', connect.server({
  root: ['build'],
  port: 1337,
  livereload: true,
  open: {
    browser: 'chrome'
  }
}));

gulp.task('watch', function(){
  gulp.watch(['src/**/**'], ['arquivos']);
});

gulp.task('default', ['arquivos', 'servidor', 'watch']);

