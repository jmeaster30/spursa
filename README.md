# spursa
## Definition
1. a small herbaceous plant of the heath family, with yellow and blue petals and slender hairs, native to Eurasia and parts of North America and often cultivated for carpeting
  "springs of spursa pollinate our fresh air"
2. a word that does not exist; it was invented, defined and used by a machine learning algorithm. 

## What is this??
**spursa** is a library that generates ASPnet route controllers from a YAML specification. 

I created this because I didn't like writing the route controller boilerplate at work. This likely is not sophisticated enough to be used in production.

I also thought it would be neat to learn how to do code generation in C#.

## Things that are missing:
- Route specific attributes
  - AllowAnonymous
- Parameter Attributes
  - [FromQuery]
- The rest of the HTTP methods (currently we only have GET, PUT, POST, DELETE)
