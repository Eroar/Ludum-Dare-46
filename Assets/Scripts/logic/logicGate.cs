using System;
using UnityEngine;

public class logicGate : MonoBehaviour
{
    /*
        Gate is activated if all levers are in correct positions

        Use this components 'isActivated' variable, to check the state of the gate

        Light game logic implementation
        the state of the gate is updated only on lever updates

        Functions to execute on opening/closing of gate are labdas
        You can set them using other scripts
    */
    

    private bool[] states;
    private int num ;
    
    private bool prev_action;


    public bool isActivated;

    public Action openFn = delegate{return ;}; // Set lambda to execute on Open
    public Action closeFn = delegate{return ;}; // Set lambda to execute on Close

    [SerializeField] bool ControlSth;
    public Activatable target;
    public logicInput[] inputs;

    void Open(){
        isActivated = true;
        openFn();
        if(ControlSth)
            target.activate();
    }

    void Close(){
        isActivated = false;
        closeFn();
        if(ControlSth)
            target.de_activate();
    }

    public void updateState(){
        // Fire Open/Close if all inputs start/stop being correctly set
        bool allEnabled = true;
        foreach(var l in inputs){
            if( l.isCorrect() == false){
                allEnabled = false;
                break;
            }
        }

        bool action = allEnabled;

        if (action != prev_action)
            if(action)
                Open();
            else
                Close();

        prev_action = action;
        
        action = allEnabled;

    }

    void Start()
    {
        updateState();
    }
}
