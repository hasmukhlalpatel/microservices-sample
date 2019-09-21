## dotnet publish
publish creates your configuration when you publish your project and what it generates into the web.config file when the project is published.

If the <AspNetCoreHostingModel> key in the project is set to OutOfProcess or is missing, the hostingModel attribute is not generated and the application defaults to OutOfProcess.

Note that you can easily switch between modes after publishing by simply changing the value between InProcess and OutOfProcess in the web.config in the Publish folder. This can be useful for debugging if you want to log output on a failing application with verbose log settings enabled for example.

Just remember that if you change publish output it will be overwritten next time you publish again.

Cool - this single setting is all you need to change to take advantage of InProcess hosting and you'll gain a bit of extra speed connecting to your application.

//https://weblog.west-wind.com/posts/2019/Mar/16/ASPNET-Core-Hosting-on-IIS-with-ASPNET-Core-22#webconfig-change