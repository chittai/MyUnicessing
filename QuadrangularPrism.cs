using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unicessing;

public class QuadrangularPrism : UGraphics
{
    float r = 1.0f;
    UShape sphere;
    string[,] Hiragana;

    private PXCMSession session;
    private PXCMAudioSource source;
    private PXCMSpeechRecognition sr;
    private PXCMSpeechRecognition.Handler handler;
    private string voice;


    protected override void Setup()
    {
        sphere = createSphere(r,r,r);
        Hiragana = new string[,]{{"あ","0","0"}, { "い", "0", "1" }, { "う", "0", "2" }, { "え", "0", "3" }, { "お", "0", "4" },
                                 {"か","1","0"}, { "き", "1", "1" }, { "く", "1", "2" }, { "け", "1", "3" }, { "こ", "1", "4" },
                                 {"さ","2","0"}, { "し", "2", "1" }, { "す", "2", "2" }, { "せ", "2", "3" }, { "そ", "2", "4" },
                                 {"た","3","0"}, { "ち", "3", "1" }, { "つ", "3", "2" }, { "て", "3", "3" }, { "と", "3", "4" },
                                 {"な","4","0"}, { "に", "4", "1" }, { "ぬ", "4", "2" }, { "ね", "4", "3" }, { "の", "4", "4" },
                                 {"は","5","0"}, { "ひ", "5", "1" }, { "ふ", "5", "2" }, { "へ", "5", "3" }, { "ほ", "5", "4" },
                                 {"ま","6","0"}, { "み", "6", "1" }, { "む", "6", "2" }, { "め", "6", "3" }, { "も", "6", "4" },
                                 {"や","7","0"}, { "ゆ", "7", "2" }, { "よ", "7", "4" },
                                 {"ら","8","0"}, { "り", "8", "1" }, { "る", "8", "2" }, { "れ", "8", "3" }, { "ろ", "8", "4" },
                                 {"わ","9","0"}, { "を", "9", "2" }, { "ん", "9", "4" }};
        VoiceRec();
        

    }
    protected override void Draw()
    {
        for (int z = 0; z < 5; z++)
        {
            for (int x = 0; x < 10; x++)
            {
                pushMatrix();
                sphere.resetShader();

                if (voice != null)
                {
                    for (int l = 0; l < voice.Length; l++)
                    {
                        string onevoice = voice.Substring(l, 1);
                        for (int h = 0; h < Hiragana.GetLength(0); h++)
                        {
                            if (onevoice == Hiragana[h,0])
                            {
                                if (x == int.Parse(Hiragana[h, 1]) && z == int.Parse(Hiragana[h, 2]))
                                {
                                    Material mat = loadShader("Material/testMaterial");
                                    sphere.shader(mat);
                                }
                            }
                        }
                    }
                }

                sphere.fill(0, 0, x*25);
                translate(x,0,z);
                draw(sphere);
                popMatrix();
            }
        }
    }

    public void VoiceRec()
    {
        session = PXCMSession.CreateInstance();
        source = session.CreateAudioSource();
        PXCMAudioSource.DeviceInfo dinfo = null;

        source.QueryDeviceInfo(1, out dinfo);
        source.SetDevice(dinfo);
        Debug.Log(dinfo.name);

        session.CreateImpl<PXCMSpeechRecognition>(out sr);

        PXCMSpeechRecognition.ProfileInfo pinfo;
        sr.QueryProfile(out pinfo);
        pinfo.language = PXCMSpeechRecognition.LanguageType.LANGUAGE_JP_JAPANESE;
        sr.SetProfile(pinfo);

        handler = new PXCMSpeechRecognition.Handler();
        voice = null;
        handler.onRecognition = (x) => { voice = x.scores[0].sentence; Debug.Log(voice); };

        sr.SetDictation();
        sr.StartRec(source, handler);
    }

    void OnDisable()
    {
        if (sr != null)
        {
            sr.StopRec();
            sr.Dispose();
        }

        if (session != null)
            session.Dispose();
    }

}
