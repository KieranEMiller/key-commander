# Key-Commander
a project to measure, improve and analyze typing speed and accuracy 

## Current Dev Status

### UI
- [x] console app prototype
- [x] windows thick client prototype in wpf
- [ ] web UI

### Glaring Deficiencies
#### Likeness Algorithm: 
currently when the user enters a word and fat-fingers or misses one character, it results in the whole rest of the word being classified as incorrect.  

one option would be to: based on the number of short or extra characters, start adding or removing characters and pick the result with the best accuracy and assume thats what the user wanted

#### UI test coverage
wpf testing coverage needs work

#### Functionality
currently limited; base prototype is in place but would like to add features such as:
- [ ] pick certain types of characters to prioritize.  these would presumably be ones the user needs practice with
- [ ] analyze cross session/sequence performance

## Windows Thick Client Screenshots
The login screen (just takes a name)
![Login](/external_resources/ui_wpf_login.png)

Main screen showing recent sessions
![Main](/external_resources/ui_wpf_main.png)

Main input screen
![Input Screen](/external_resources/ui_wpf_input.png)
