#ATM BANK SYSTEM
balance = 0.00

print("\n ATM BANK SYSTEM")
while True: 
    print("\nATM BANK SYSTEM")
    print("1. Balance")
    print("2. Deposit")
    print("3. Withdraw")
    print("4. Exit")

    menu = input("\nSelect an option: ")

    if menu == "1":
        print(f"Current Balance: ₱{balance:,.2f}")

    elif menu == "2":
        amount = float(input("Enter Deposit Amount: "))
        balance += amount
        print(f"Successfully Deposit ₱{amount:,.2f}")

    elif menu == "3":
        amount = float(input("Enter Withdraw Amount: "))
        if amount > balance:
            print("Insufficient funds!")
        else:
            balance -= amount
            print(f"Successfully withdrawn ₱{amount:,.2f}")

    elif menu == "4":
        print("Thank you for Banking with us. Have a great day!")
        break
