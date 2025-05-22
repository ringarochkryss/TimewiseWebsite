# Timewise Website
Published on Heroku: https://timewisewebsite-2a9360ab49e3.herokuapp.com/

## Users and Roles
This page has login and registry functionality.

- The website handles as many roles and users as needed but two particular roles are required: one for admin and one for customers.
- When logged in as admin, several admin buttons are available on the index page, making it possible to add, edit, and delete almost all content on this website.
- Admins also have access to admin functions spread out across the app.
- Users logged in as customers can access the learn platform.
- Users who register themselves need to be given a role by an admin to access more material on the site.

## Languages
This website is written in English and Swedish. Language is selected based on the browser language. Users can also manually select language in the navbar. Articles can be written with manual translations. Instructions in the learn platform only have one language. To handle more languages in the Learn platform, categories or icons can be used.

## Blog Articles
This app displays blog articles categorized with tags.

- The articles contain one image, headers, and text.
- Each tag is displayed as a button in the navbar.
- On user click, all articles with that tag are shown on a page.
- The user can click an article to see the full text.
- Users with admin rights can create, edit, and delete articles and tags.
- Users don’t need to log in to see blog articles.
- Articles marked “featured” are displayed on the index page.
- Only one article can be “featured”. If admin sets another article as featured, the previously featured article will automatically no longer be featured.

## Contact Form
The contact form is accessed from the navbar. Users can fill it in and send it, generating an email to `support@timewise.se`. Sent messages are stored in the admin section for contacts. Admin can read and delete these messages here at any time.

## Prices
This prices page, linked from the navbar, shows different price levels for future customers of Timewise. Each price level is displayed in a column with customized design. Admin can create, edit, and delete the content from this page. In current deploy Prices page is hidden for all but admin since it's not needed.

## Learn Platform
The Learn platform makes all our instructions accessible for our customers.

- Admin can create learning material from the admin tools on the index site.
- Instructions can either be movies or step-by-step instructions.
- The instructions are structured in categories and marked with desktop and/or web.
- The learn platform sorts all categories by sort order and displays the instructions in cards, one for each category. From here, the user can easily click on the instructions to open them. Step-by-step instructions can be printed.
- Customers have the possibility to create wishes on the Learn Platform. When they add a wish, they actually create a new instruction but are restricted to add just the name formed as a heading or a question. Admin can then open these wishes and complete the instruction. Wishes is hidden in the current deploy.

## Technical Description
- The app is written in Razor Pages (C#.NET) with jQuery and JavaScript.
- mySQL database
- Bootstrap 5
- Google font Quicksand and icons from Font Awesome 6
- Published on heroku: https://timewisewebsite-2a9360ab49e3.herokuapp.com/


## Startup
Starting this app for the first time will create a database. First time role and admin user need to be added manually, either in the database or by commenting out admin restrictions on the right div on the index page. The second option is the easiest, just remember to undo the changes to the index page afterwards.
