/// <binding />
var gulp = require("gulp");
var runSequence = require("run-sequence");
var msbuild = require("gulp-msbuild");
var foreach = require("gulp-foreach");
var debug = require("gulp-debug");
var config = require("./gulp-config.js")();

gulp.task("01.Publish-All-Projects", function (callback) {
    return runSequence(
      "Build-Solution",
      "Publish-Project-Projects",
      "Publish-Foundation-Projects",
      "Publish-Feature-Projects", callback);
});

gulp.task("Build-Solution", function () {
    var targets = ["Build"];
    if (config.runCleanBuilds) {
        targets = ["Clean", "Build"];
    }
    var solution = "./" + config.solutionName + ".sln";
    return gulp.src(solution)
        .pipe(msbuild({
            targets: targets,
            configuration: config.buildConfiguration,
            logCommand: false,
            verbosity: "minimal",
            stdout: true,
            errorOnFail: true,
            maxcpucount: 0,
            toolsVersion: 14.0
        }));
});

gulp.task("Publish-Project-Projects", function () {
    return publishProjects("./src/Project");
});

gulp.task("Publish-Foundation-Projects", function () {
    return publishProjects("./src/Foundation");
});

gulp.task("Publish-Feature-Projects", function () {
    return publishProjects("./src/Feature");
});

var publishProjects = function (location, dest) {
    dest = dest || config.websiteRoot;
    var targets = ["Build"];

    console.log("publish to " + dest + " folder");
    return gulp.src([location + "/**/code/*.csproj"])
      .pipe(foreach(function (stream, file) {
          return stream
            .pipe(debug({ title: "Building project:" }))
            .pipe(msbuild({
                targets: targets,
                configuration: config.buildConfiguration,
                logCommand: false,
                verbosity: "minimal",
                stdout: true,
                errorOnFail: true,
                maxcpucount: 0,
                toolsVersion: 14.0,
                properties: {
                    DeployOnBuild: "true",
                    DeployDefaultTarget: "WebPublish",
                    WebPublishMethod: "FileSystem",
                    DeleteExistingFiles: "false",
                    publishUrl: dest,
                    _FindDependencies: "false"
                }
            }));
      }));
};