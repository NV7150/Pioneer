﻿using System.Collections;


    private GameObject tradeWindowPrefab;
                if (Input.GetKeyDown(KeyCode.Return)) {
                    if (massageList.Count > massageIndex) {
                        if (massagePrinting) {
                            //メッセージをプリントしている場合は全文字表示してコルーチン停止
                            Debug.Log("into massagePringing true");
                            cancelPrint();
                        } else {
                            //違うなら新しく文字を表示
                            Debug.Log("into massagePringing false");
                            printCoroutine = StartCoroutine("showText");
                        }
                    } else {
                        //インデックスが最後まで来たら削除
                        TalkManager.getInstance().finishTalk();
                        Destroy(view);
                    }
                    
                }
        TradeWindow tradeWindow = MonoBehaviour.Instantiate(tradeWindowPrefab).GetComponent<TradeWindow>();
        tradeWindow.setState(tradegoods, player, trader);
        //かり
        tradeWindow.transform.SetParent(GameObject.Find("WindowNode").transform);
    }
        massageList = massage;
        stopIndex = stop;
    }

    }