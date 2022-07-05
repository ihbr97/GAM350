using System;
using System.Collections.Generic;
using System.Text;

namespace EosScriptCore.Scripts
{
	public class BridgeInput : MonoBase
	{
		List<VirusScript> inputs = new List<VirusScript>();
		
		public BridgeSource linkedBridge;
		public GameObject detect;
		public GameObject player;

        bool canUpdate = false;
		bool inputFlag = false;

        private AudioComponent audiocomp;

        void UpdateBridge()
		{
            List<Color> colorList = new List<Color>();

            for (int i = 0; i < inputs.Count; ++i)
            {
                colorList.Add(inputs[i].currentcolor);
            }

			BridgeSource.globalController.BuildBridge(colorList);
		}

		void OnStart()
		{
			player = gameObject.FindWithTag("Surge");
            audiocomp = gameObject.GetComponent<AudioComponent>();
            //colorList = new List<Color>();
        }

		void OnUpdate()
		{
            if (canUpdate)
            {
                if (!inputFlag)
                {
                    if (PlayerMovement.globalController.GetButtonA())
                    {
                        if (VirusReorderScript.globalController.playerViruses.Count != 0)
                        {
                            audiocomp.PlaySFX("Insert_Data.ogg");
                            VirusScript v = VirusReorderScript.globalController.playerViruses[0];
                            inputs.Add(v);
                            v.gameObject.transform.position = transform.position;
                            v.InputIntoMechanic();
                            inputFlag = true;
                            //colorList.Add(Color.Red);
                            UpdateBridge();
                        }
                        Log.LogInfo("Z pressed");
                    }

                    if (PlayerMovement.globalController.GetButtonB())
                    {
                        if (inputs.Count != 0)
                        {
                            VirusScript v = inputs[inputs.Count - 1]; //get last input
                            inputs.Remove(v);
                            v.Obtain();
                            inputFlag = true;
                            audiocomp.PlaySFX("Remove_Data.ogg");
                            //if (colorList.Count != 0)
                            //{
                            //	Color v = colorList[colorList.Count - 1];
                            //	colorList.Remove(v);
                            //}
                            //colorList.Add(Color.Blue);
                            UpdateBridge();
                        }
                        Log.LogInfo("X pressed");
                    }
                }
                else
                {
                    if (!PlayerMovement.globalController.GetButtonA() && !PlayerMovement.globalController.GetButtonB())
                    {
                        inputFlag = false;
                    }
                }
            }
            if (PlayerMovement.globalController.checkpointed)
            {
                while (inputs.Count > 0)
                {
                    VirusScript v = inputs[inputs.Count - 1];
                    inputs.Remove(v);
                    v.Obtain();
                }
                UpdateBridge();
            }
        }
		
		void OnFixedUpdate()
		{
			
		}
		
		void OnTriggerBegin(ulong data)
        {

        }

        //ASK IRFAN
        void OnTriggerStay(ulong data)
        {
            //if (player.GetComponent<Collider>().isTrigger)
            //{
            //    if (!inputFlag)
            //    {
            //        if (Input.IsKeyPressed(KeyCode.Z))
            //        {
            //            VirusScript v = VirusReorderScript.globalController.playerViruses[0];
            //            inputs.Add(v);
            //            v.gameObject.transform.position = transform.position;
            //            v.InputIntoMechanic();
            //            inputFlag = true;
            //            UpdateBridge();
            //        }

            //        if (Input.IsKeyPressed(KeyCode.X))
            //        {
            //            VirusScript v = inputs[inputs.Count - 1]; //get last input
            //            inputs.Remove(v);
            //            v.Obtain();
            //            inputFlag = true;
            //            UpdateBridge();
            //        }
            //    }
            //    else
            //    {
            //        if (!Input.IsKeyPressed(KeyCode.Z) && !Input.IsKeyPressed(KeyCode.X))
            //        {
            //            inputFlag = false;
            //        }
            //    }
            //}

            if (player.GetComponent<Collider>().isTrigger)
            {
                canUpdate = true;
            //    if (!inputFlag)
            //    {
            //        if (Input.IsKeyPressed(KeyCode.Z))
            //        {
            //            VirusScript v = VirusReorderScript.globalController.playerViruses[0];
            //            inputs.Add(v);
            //            v.gameObject.transform.position = transform.position;
            //            v.InputIntoMechanic();
            //            inputFlag = true;
            //            //colorList.Add(Color.Red);
            //            UpdateBridge();
            //            Log.LogInfo("Z pressed");
            //        }

            //        if (Input.IsKeyPressed(KeyCode.X))
            //        {
            //            VirusScript v = inputs[inputs.Count - 1]; //get last input
            //            inputs.Remove(v);
            //            v.Obtain();
            //            inputFlag = true;
            //            //if (colorList.Count != 0)
            //            //{
            //            //	Color v = colorList[colorList.Count - 1];
            //            //	colorList.Remove(v);
            //            //}
            //            //colorList.Add(Color.Blue);
            //            UpdateBridge();
            //            Log.LogInfo("X pressed");
            //        }
            //    }
            //    else
            //    {
            //        if (!Input.IsKeyPressed(KeyCode.Z) && !Input.IsKeyPressed(KeyCode.X))
            //        {
            //            inputFlag = false;
            //        }
            //    }
            }
        }

		void OnTriggerEnd(ulong data)
        {
            canUpdate = false;
        }
	}
}