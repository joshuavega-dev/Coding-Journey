customer_name = input ("Enter Your Name: ")
item = input ("Enter Item Name: ")
cost = float (input ("Enter Item Cost: "))
quantity = int(input("Enter Item Quantity: "))
total = cost * quantity 

#discount student,pwd,senior
if total > 100:
    discount = total * .10
    total = total - discount
    print ("Congrats you received a 10% discount!")

elif total > 60:
    discount = total * 0.06
    total = total - discount 
    print ("Congrats you received a 6% discount!")

else:
    print ("Thank you for the purchase!")   

print (f"You successfully bought {quantity} x {item}!")
print (f"total bill {total:.2f}$")
print ("Thank you. so much for the purchase!")
