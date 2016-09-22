### **Keeping .NET app settings secrets out of source control**

Whether your app is a desktop client app or a website, it’s important to keep passwords, connection strings, and API keys out of source control. In .NET, these settings are stored in a *app.config* or *web.config* file depending on the type of app you are building and those files would be checked into source control. I’ve done this in the past for many projects. Of course, I never committed production secrets, but it was still a big no-no, but I just never had the time to investigate the proper way to handle this situation…until today.

A quick Google search returned this result: [http://www.asp.net/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure](http://www.asp.net/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure). Config files have a simple way of handling this by adding a **file** attribute to the **appSettings **section of the *web.config* file, like so 

```
<appSettings file="secrets.config">
  <add key="testSetting" value="not a secret" />
</appSettings/>
```

And the *secrets.config* file looks like this

```
<appSettings>
  <add key="secretTestSetting" value="very secret" />
  <add key="testSetting" value="I will overwrite" />
</appSettings>
```

This is the entire file, it is important that the root element is **appSettings**, otherwise you will get a compile or runtime error. Any new keys defined will be added and any existing keys will overwrite the value from the *web.config*. 

Overwriting values in the *web.config* is very useful for local development. Since the *secrets.config* file is never checked in, each developer can keep their own local app settings values without worrying about mistakenly checking them in and overriding the *web.config* default values. No more commenting out values in your *web.config*! 

The same thing works for the *connectionStrings* section, except in this case the entire section is overwritten. In fact, the *web.config* can’t have any elements under it or you will get errors. The *web.config* will look like this

```
<connectionStrings configSource="connectionStrings.config">
</connectionStrings>
```

And the *connectionStrings.config* will look like this

```
<connectionStrings>
  <clear/>
  <add name="Database" connectionString="very secret" />
</connectionStrings>
```

As long as your extra files have the **.config** extension, IIS will never serve them. Also, you should never add these secrets config files to your project to avoid mistakenly deploying them to your servers. Instead, your release process should deploy secrets files separately from code files. 

And finally, always add **secrets.config** and **connectionStrings.config** to your *.gitignore* and to get first-time developers setup with secrets, you could include these files somewhere outside of source control, like a file share.

That’s all! Super easy way to keep all your secrets safe in both desktop and web applications. 