# Key-Commander
a project to measure, improve and analyze typing speed and accuracy 

## Current Dev Status

### UI
- [x] console app prototype
- [x] windows thick client prototype in wpf
- [x] web UI
- [ ] web UI: improve responsiveness of as-you-type analysis
- [ ] implement additional functionality as described below

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

