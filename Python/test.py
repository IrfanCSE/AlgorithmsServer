from testFunc import *

# val=Main.run()
# output = "Hello From Python"
# output=val
# print (output)
# val = 'c#'

class Main:
    def fun(self,val):
        output = "Hello From Python"
        ex = self.run()
        out = testFunc().run()
        output += val
        return output+ex+out

    def run(self):
        return "extra"


# print(main(val))
