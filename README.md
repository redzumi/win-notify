win-notify
================

Simple notifications app (win10).

## Usage
```
win-notify.exe --app TestApp --timeout false --xml "<toast activationType='protocol' launch='http://google.com/'><visual><binding template='ToastGeneric'><text>Title</text><text>Body</text><image placement='appLogoOverride' src='file:///C:/test.jpg' hint-crop='circle' /></binding></visual></toast>"
```

## Command line arguments
[[kb830473](http://support.microsoft.com/kb/830473)]
```
required arguments:
  --app            	APP_ID for notification (will be shown in the notification bottom)
  --timeout			true or false (expectation for dismissing/activating event)
  --xml				Notification template
```

## Notification template
[[blogs.msdn.microsoft.com](https://blogs.msdn.microsoft.com/tiles_and_toasts/2015/07/02/adaptive-and-interactive-toast-notifications-for-windows-10/)]

```
<toast activationType='protocol' launch='http://google.com/'>
  <visual>
    <binding template='ToastGeneric'>
      <text>Title</text>
      <text>Body</text>
      <image placement='appLogoOverride' src='file:///C:/test.jpg' hint-crop='circle' />
    </binding>
  </visual>
</toast>
```
*TODO: another way of receiving notification template (?)*
***
*Like a [toaster](https://github.com/nels-o/toaster), but without shortcut (only win10?) and with custom app id.*

