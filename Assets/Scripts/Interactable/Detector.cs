using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Detector : MonoBehaviour
    {
        private List<Interactable> interactables { get; } = new List<Interactable>();
        private List<Interactable> interactables_temp { get; } = new List<Interactable>();

        private Vector3 playerPos { get { return GM.player.position; } }

        public int numberDetectedObject;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Detect());
        }

        private IEnumerator Detect()
        {
            while(true)
            {
                yield return GM.waitForThirdSecond;

                var obj = Physics.OverlapSphere(playerPos, 1.5f, GM.LayerInteractableObject);

                if(obj.Length > 0)
                {
                    for(int i = 0; i < obj.Length; i++)
                    {
                        bool foundSame = false;

                        for (int j = 0; j < interactables.Count; j++)
                        {
                            if (interactables[j].collider == obj[i])
                            {
                                foundSame = true;
                                break;
                            }
                        }

                        if (!foundSame)
                        {
                            var interactable = obj[i].GetComponent<Interactable>();
                            if (interactable.isMoving)
                                continue;
                            interactable.enabled = true;
                            interactables_temp.Add(interactable);
                        }
                    }

                    if(interactables_temp.Count > 0)
                    {
                        for (int i = 0; i < interactables.Count; i++)
                        {
                            if (!interactables_temp.Contains(interactables[i]))
                            {
                                interactables.RemoveAt(i);
                                continue;
                            }
                        }
                    }

                    interactables.AddRange(interactables_temp);

                    interactables_temp.Clear();
                }
                else
                {
                    if(interactables.Count > 0)
                    {
                        foreach (Interactable i in interactables)
                            if(!i.isMoving)
                                i.enabled = false;

                        interactables.Clear();
                    }
                }

                numberDetectedObject = interactables.Count;
            }
        }
    }

}
