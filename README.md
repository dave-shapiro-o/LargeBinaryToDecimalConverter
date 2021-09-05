# LargeBinaryToDecimalConverter
This started as an exercise from my C# course. I decided to try to extend its capabilities.

Accepting the binary number as a string, it was able to convert arbitrarily large numbers, but was very slow.
I came up with the idea of:

  Breaking the binary string into substrings (after a little trial and error, a size of 2048 digits seemed optimal)
  
  Converting each one as if it were in the lowest (rightmost) position
  
  After conversion, multiplying each by the correct power of 2 according to its position

This sped the processing time up quite a bit!
