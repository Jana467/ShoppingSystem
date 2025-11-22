namespace ShoppingSystem
{
    internal class Program
    {
        static public List<string> Cart=new List<string>();
        static public Dictionary<string,double> ItemPrice = new Dictionary<string,double>()
        {
            {"Camera",2000 },
            {"Microphone",1300 },
            {"MobilePhone",4500 },
            {"Laptop",12000 },
            {"Airpods",1500 }
        };
        static public Stack<string> actions = new Stack<string>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("============================");
                Console.WriteLine("1.Add item to cart");
                Console.WriteLine("2.View cart");
                Console.WriteLine("3.Remove item from cart");
                Console.WriteLine("4.Checkout");
                Console.WriteLine("5.Undo Last Action");
                Console.WriteLine("6.Exit");
                Console.WriteLine("============================");
                Console.WriteLine("Enter a number");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddItem();
                        break;
                    case 2:
                       ViewCart();
                        break;
                    case 3:
                        RemoveItem();
                        break;
                    case 4:
                        Checkout();
                        break;
                    case 5:
                        UndoLastAction();
                        break;
                    case6:
                        Environment.Exit(0);
                        break;
                    case 7:
                        Console.WriteLine("Invalid number please, Try Again");
                        break;
                }
            }
            
        }

        private static void UndoLastAction()
        {
            if(actions.Any())
            {
                string lastaction = actions.Pop();
                Console.WriteLine($"your last action is {lastaction}");
                var arraysplited=lastaction.Split();
                if(lastaction.Contains("added"))
                {
                  Cart.Remove(arraysplited[1]);
                }
                else if(lastaction.Contains("removed"))
                {
                    Cart.Add(arraysplited[1]);
                }
                else
                {
                    Console.WriteLine("Checkout cannot be undo you can make refund");
                }
            }
        }

        private static void Checkout()
        {
            double totalamount = 0;
            var itempricescollection = GetPricesCart();
            foreach (var item in itempricescollection)
            {
                totalamount += item.Item2;
            }
            Console.WriteLine($"Total amount to be paid is {totalamount}"); 
            Console.WriteLine("****Thank you for shopping with us!****");
            Cart.Clear();
            actions.Push("Checkout!");
        }

        private static void RemoveItem()
        {
            ViewCart();
            if(Cart.Any())
            {
                Console.WriteLine("Enter the item to be removed");
                string removeditem = Console.ReadLine();
                if (Cart.Contains(removeditem))
                {
                    Cart.Remove(removeditem);
                    actions.Push($"Item {removeditem} removed from the cart");
                    Console.WriteLine($"{removeditem} removed from the cart");
                }
                else
                {
                    Console.WriteLine("Item not found in the cart");
                }
            }
        }

        private static void ViewCart()
        {
            if(Cart.Any() == false)
            {
                Console.WriteLine("Cart is empty");
                return;
            }
            else
            {
                var itempricescollection = GetPricesCart();
                foreach (var item in itempricescollection)
                {
                    Console.WriteLine($"Item {item.Item1},Prices {item.Item2}");
                }
            }
        }
        private static IEnumerable<Tuple<string,double>> GetPricesCart()
        {
            var PricesInCart = new List<Tuple<string, double>>();
            foreach (var item in Cart)
            {
                double price=0;
                bool itemprice=ItemPrice.TryGetValue(item,out price);
                if(itemprice)
                {
                    Tuple<string, double> itemsavaliable = new Tuple<string, double>(item,price);
                    PricesInCart.Add(itemsavaliable);
                }
            }
            return PricesInCart;
        }

        private static void AddItem()
        {
            Console.WriteLine("Avaliable Items");
            foreach(var item in ItemPrice)
            {
                Console.WriteLine($"Item {item.Key} Price {item.Value}");
            }
            Console.WriteLine("Choose an item");
            string chosenitem = Console.ReadLine();
            if(ItemPrice.ContainsKey(chosenitem))
            {
                Cart.Add(chosenitem);
                actions.Push($"Item {chosenitem} added to the cart");
                Console.WriteLine($"{chosenitem} added to the cart");
            }
            else
            {
                Console.WriteLine("Item unavaliable");
            }
        }
    }
}
