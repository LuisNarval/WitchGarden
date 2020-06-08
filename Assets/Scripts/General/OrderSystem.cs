using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{

    [Header("Flux")]
    [Header("CONFIG")]
    public int maxSimultaneousOrders;
    public int minTimeBetweenOrders;
    public int maxTimeBetweenOrders;
    public int minPlantPerBasket;
    public int maxPlantPerBasket;

    [Header("Species to Use")]
    public bool Belladona;
    public bool DragonMouth;
    public bool Lavanda;
    public bool Mandragora;
    public bool Orkiller;
    public bool Stronium;


    [Header("REFERENCE")]
    public Basket[] baskets;
    public RectTransform[] positions;


    [Header("REFERENCE TO PROYECT")]
    public Kind kBelladona;
    public Kind kDragonMouth;
    public Kind kLavanda;
    public Kind kMandragora;
    public Kind kOrkiller;
    public Kind kStronium;


    [Header("QUERY")]
    public List<Kind> kindList;


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
        initList();
        initOrders();
    }


    void initList()
    {
        kindList = new List<Kind>();
        if (Belladona)   kindList.Add(kBelladona);
        if (DragonMouth) kindList.Add(kDragonMouth);
        if (Lavanda)     kindList.Add(kLavanda);
        if (Mandragora)  kindList.Add(kMandragora);
        if (Orkiller)    kindList.Add(kOrkiller);
        if (Stronium)    kindList.Add(kStronium);
    }


    public void initOrders()
    {
        orders = new OrdersTemplate[6];
        isOccupied = new bool[6];

        for (int i = 0; i < baskets.Length; i++){
            orders[i].basket = baskets[i];
            orders[i].wasSend = false;
        }
    }

    public void sendOrders(){
        StopAllCoroutines();
        StartCoroutine(coroutineSendOrders());
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

    }
    
    public void endGame(){
        StopAllCoroutines();
        for (int i = 0; i < orders.Length; i++){
            if (orders[i].wasSend)
                orders[i].basket.endGame();
        }
    }

    /*public void storeAllOrders()
    {
        StopAllCoroutines();
        for (int i = 0; i < orders.Length; i++){
            if (orders[i].wasSend)
                orders[i].basket.fullBasket();
        }
    }*/



    void sendNewOrder(){

        int randomSlot;
        int randomBasket;
        int randomKind;

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


        randomKind = (int)Random.Range(0.0f, kindList.Count);

        orders[randomBasket].basket.sendBasket(minPlantPerBasket,maxPlantPerBasket, randomSlot, randomBasket, kindList[randomKind]);
    }




    public static void releaseBasket(int slot, int basket){
        isOccupied[slot] = false;
        orders[basket].wasSend = false;
        currentOrders--;
    }




}