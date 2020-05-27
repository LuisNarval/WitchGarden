using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{

    [Header("CONFIG")]
    public int maxSimultaneousOrders;
    public int minTimeBetweenOrders;
    public int maxTimeBetweenOrders;
    public int maxPlantPerBasket;

    [Header("REFERENCE")]
    public Basket[] baskets;
    public RectTransform[] positions;


    static int currentOrders;
    static bool[] isOccupied;


    struct OrdersTemplate
    {
        public Basket basket;
        public bool wasSend;
    }

    static OrdersTemplate[] orders;


    // Start is called before the first frame update
    void Start(){
        initOrders();
        StartCoroutine(coroutineSendOrders());   
    }


    void initOrders()
    {
        orders = new OrdersTemplate[6];
        isOccupied = new bool[6];

        for (int i = 0; i < baskets.Length; i++){
            orders[i].basket = baskets[i];
            orders[i].wasSend = false;
        }

    }


    IEnumerator coroutineSendOrders()
    {
        yield return new WaitForSeconds(3.0f);

        while (TimeSystem.time>0)
        {
            if (currentOrders < maxSimultaneousOrders){
                sendNewOrder();
                currentOrders++;
            }
            yield return new WaitForSeconds(Random.Range(minTimeBetweenOrders,maxTimeBetweenOrders));
        }


        for (int i = 0; i < orders.Length; i++) {
            if (orders[i].wasSend)
                orders[i].basket.fullBasket();
        }

    }


    void sendNewOrder(){

        int randomSlot;
        int randomBasket;

        do{
            randomSlot = (int)Random.Range(0.0f, 6.0f);
        } while (isOccupied[randomSlot] != false);

        do{
            randomBasket = (int)Random.Range(0.0f, 6.0f);
        } while (orders[randomBasket].wasSend != false);

        isOccupied[randomSlot] = true;
        orders[randomBasket].wasSend = true;

  

        RectTransform rT = orders[randomBasket].basket.gameObject.GetComponent<RectTransform>();
        rT.position= positions[randomSlot].position;


        orders[randomBasket].basket.sendBasket(maxPlantPerBasket, randomSlot, randomBasket);
    }




    public static void releaseBasket(int slot, int basket){
        isOccupied[slot] = false;
        orders[basket].wasSend = false;
        currentOrders--;
    }




}