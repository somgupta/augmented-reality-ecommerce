using UnityEngine;

public enum skinselect {SkinA,SkinB,SkinC};
public enum eyesselect {Type0,Type1,Type2,Type3,Type4,Type5,Type6,Type7,Type8,Type9,Type10,Type11,Type12,Type13,Type14,Type15};
public enum lodselect {Auto=-1,Lod_0,Lod_1,Lod_2};

public class camera_cs : MonoBehaviour
{
	//dinos script
	//anky_cs anky;
	//brach_cs brach;
	comp_cs comp;
	//dilo_cs dilo;
	//dime_cs dime;
	//ovi_cs ovi;
	//para_cs para;
	//ptera_cs ptera;
	//rap_cs rap;
	//rex_cs rex;
	//spino_cs spino;
	//steg_cs steg;
	//tric_cs tric;
	//arge_cs arge;
	//pachy_cs pachy;
	
	int cammode=0; //camera mode : 0=chase cam, 1=free cam, 2=locked cam
	bool wireframe,gui; //wireframe mode, hide/show gui
	float yadd,distance,zoom, x, y; //camera position
	Vector3 distanceVector; //distance to the target
	float timer, frame, fps; //variables used for fps counter

	[SerializeField]Transform[] target; //target of the camera
	string name,infos; //full dino name and mesh infos
	int lod, body, eyes, dino; //store dino infos
	float scale;
	bool load=false, AI=false;
	
	
	//***************************************************************************************
	//Camera behavior
	void Update ()
	{
		//Very simple Fps counter 
		frame += 1.0f;
		timer += Time.deltaTime;
		if(timer>1.0f) { fps = frame; timer = 0.0F; frame = 0.0F; }

		// Zoom or dezoom using mouse wheel
		if ( Input.GetAxis("Mouse ScrollWheel") > 0.0f)
		{
			if(zoom<distance)zoom += 2.0f;
			distanceVector = new Vector3(0.0f,yadd*target[dino].localScale.x,(-distance+zoom)*target[dino].localScale.x);
		}
		else if ( Input.GetAxis("Mouse ScrollWheel") < 0.0f)
		{
			if(zoom<100)zoom -= 2.0f;
			distanceVector = new Vector3(0.0f,yadd*target[dino].localScale.x,(-distance+zoom)*target[dino].localScale.x);
		}
		else distanceVector = new Vector3(0.0f,yadd*target[dino].localScale.x,(-distance+zoom)*target[dino].localScale.x);

		if(cammode ==0) // free cam
		{
			// rotate the camera when the middle mouse button is pressed
			if(Input.GetKey(KeyCode.Mouse2))
			{
				x += Input.GetAxis("Mouse X") * 5.0F;
				y += -Input.GetAxis("Mouse Y")* 5.0F;
			}
			
			Quaternion rotation = Quaternion.Euler(y,x,0.0f);
			Vector3 position = rotation * distanceVector + target[dino].position;
			transform.rotation = rotation;
			transform.position = position;
		}
		else if(cammode ==1) // chase cam
		{
			// rotate the camera when the middle mouse button is pressed
			if(Input.GetKey(KeyCode.Mouse2)) y -= Input.GetAxis("Mouse Y")*5.0F;
			
			if(Input.GetKey(KeyCode.Mouse2)) x += Input.GetAxis("Mouse X")*10.0f;
			// reset camera rotation
			else x = Mathf.LerpAngle(x,target[dino].eulerAngles.y,0.05f);
			
			
			Quaternion rotation = Quaternion.Euler(target[dino].eulerAngles.z+y, x, 0.0f);
			Vector3 position = rotation * distanceVector + target[dino].position;
			transform.rotation = rotation;
			transform.position = position;
		}
		else // locked cam
			transform.LookAt(target[dino].transform);
	}
	

	//***************************************************************************************
	//Gui
	void OnGUI ()
	{
		//Show or hide GUI
		if(Input.mousePosition.x<5) gui=true;
		else if(Input.mousePosition.x>240) gui=false;

		//Get dino script
		switch (target[dino].GetChild(0).name)
		{
		case "Comp": if(!load) { comp=target[dino].GetComponent<comp_cs>(); name = "Compsognathus"; yadd=2; distance=5;
			infos=comp.infos; AI=comp.AI; body=comp.BodySkin.GetHashCode(); eyes=comp.EyesSkin.GetHashCode(); lod=comp.LodLevel.GetHashCode(); scale=comp.scale; load=true;}
			else if(gui) { infos = comp.infos; comp.AI=AI; comp.BodySkin = (skinselect) body; comp.EyesSkin = (eyesselect) eyes; comp.LodLevel = (lodselect) lod; comp.scale=scale;}
			break;
			/*
		case "Rap": if(!load) { rap=target[dino].GetComponent<rap_cs>(); name = "Velociraptor"; yadd=7; distance=10;
			infos=rap.infos; AI=rap.AI; body=rap.BodySkin.GetHashCode(); eyes=rap.EyesSkin.GetHashCode(); lod=rap.LodLevel.GetHashCode(); scale=rap.scale;load=true;}
			else if(gui) { infos = rap.infos; rap.AI=AI; rap.BodySkin = (skinselect) body; rap.EyesSkin = (eyesselect) eyes; rap.LodLevel = (lodselect) lod; rap.scale=scale;}
			break;
		case "Ovi": if(!load) { ovi=target[dino].GetComponent<ovi_cs>(); name = "Oviraptor"; yadd=7; distance=10; 
			infos=ovi.infos; AI=ovi.AI; body=ovi.BodySkin.GetHashCode(); eyes=ovi.EyesSkin.GetHashCode(); lod=ovi.LodLevel.GetHashCode(); scale=ovi.scale; load=true;}
			else if(gui) { infos = ovi.infos; ovi.AI=AI; ovi.BodySkin = (skinselect) body; ovi.EyesSkin = (eyesselect) eyes; ovi.LodLevel = (lodselect) lod; ovi.scale=scale;}
			break;
		case "Dilo": if(!load) { dilo=target[dino].GetComponent<dilo_cs>(); name = "Dilophosaurus"; yadd=7; distance=10;
			infos=dilo.infos; AI=dilo.AI; body=dilo.BodySkin.GetHashCode(); eyes=dilo.EyesSkin.GetHashCode(); lod=dilo.LodLevel.GetHashCode(); scale=dilo.scale; load=true;}
			else if(gui) { infos = dilo.infos; dilo.AI=AI; dilo.BodySkin = (skinselect) body; dilo.EyesSkin = (eyesselect) eyes; dilo.LodLevel = (lodselect) lod; dilo.scale=scale;}
			break;
		case "Rex": if(!load) { rex=target[dino].GetComponent<rex_cs>(); name = "Tyrannosaurus Rex"; yadd=20; distance=35;
			infos=rex.infos; AI=rex.AI; body=rex.BodySkin.GetHashCode(); eyes=rex.EyesSkin.GetHashCode(); lod=rex.LodLevel.GetHashCode(); scale=rex.scale;load=true;}
			else if(gui) { infos = rex.infos; rex.AI=AI; rex.BodySkin = (skinselect) body; rex.EyesSkin = (eyesselect) eyes; rex.LodLevel = (lodselect) lod; rex.scale=scale;}
			break;
		case "Spino": if(!load) { spino=target[dino].GetComponent<spino_cs>(); name = "Spinosaurus"; yadd=20; distance=35;
			infos=spino.infos; AI=spino.AI; body=spino.BodySkin.GetHashCode(); eyes=spino.EyesSkin.GetHashCode(); lod=spino.LodLevel.GetHashCode(); scale=spino.scale; load=true;}
			else { infos = spino.infos; spino.AI=AI; spino.BodySkin = (skinselect) body; spino.EyesSkin = (eyesselect) eyes; spino.LodLevel = (lodselect) lod; spino.scale=scale;}
			break;
		case "Tric": if(!load) { tric=target[dino].GetComponent<tric_cs>(); name = "Triceratops"; yadd=15; distance=20;
			infos=tric.infos; AI=tric.AI; body=tric.BodySkin.GetHashCode(); eyes=tric.EyesSkin.GetHashCode(); lod=tric.LodLevel.GetHashCode(); scale=tric.scale; load=true;}
			else if(gui) { infos = tric.infos; tric.AI=AI; tric.BodySkin = (skinselect) body; tric.EyesSkin = (eyesselect) eyes; tric.LodLevel = (lodselect) lod; tric.scale=scale;}
			break;
		case "Brach": if(!load) { brach=target[dino].GetComponent<brach_cs>(); name = "Brachiosaurus"; yadd=20; distance=40;
			infos=brach.infos; AI=brach.AI; body=brach.BodySkin.GetHashCode(); eyes=brach.EyesSkin.GetHashCode(); lod=brach.LodLevel.GetHashCode(); scale=brach.scale; load=true;}
			else if(gui) { infos = brach.infos; brach.AI=AI; brach.BodySkin = (skinselect) body; brach.EyesSkin = (eyesselect) eyes; brach.LodLevel = (lodselect) lod; brach.scale=scale;}
			break;
		case "Dime": if(!load) { dime=target[dino].GetComponent<dime_cs>(); name = "Dimetrodon"; yadd=10; distance=15;
			infos=dime.infos; AI=dime.AI; body=dime.BodySkin.GetHashCode(); eyes=dime.EyesSkin.GetHashCode(); lod=dime.LodLevel.GetHashCode(); scale=dime.scale; load=true;}
			else if(gui) { infos = dime.infos; dime.AI=AI; dime.BodySkin = (skinselect) body; dime.EyesSkin = (eyesselect) eyes; dime.LodLevel = (lodselect) lod; dime.scale=scale;}
			break;
		case "Para": if(!load) { para=target[dino].GetComponent<para_cs>(); name = "Parasaurolophus"; yadd=15; distance=20;
			infos=para.infos; AI=para.AI; body=para.BodySkin.GetHashCode(); eyes=para.EyesSkin.GetHashCode(); lod=para.LodLevel.GetHashCode(); scale=para.scale; load=true;}
			else if(gui) { infos = para.infos; para.AI=AI; para.BodySkin = (skinselect) body; para.EyesSkin = (eyesselect) eyes; para.LodLevel = (lodselect) lod; para.scale=scale;}
			break;
		case "Steg": if(!load) { steg=target[dino].GetComponent<steg_cs>(); name = "Stegosaurus"; yadd=15; distance=20;
			infos=steg.infos; AI=steg.AI; body=steg.BodySkin.GetHashCode(); eyes=steg.EyesSkin.GetHashCode(); lod=steg.LodLevel.GetHashCode(); scale=steg.scale; load=true;}
			else if(gui) { infos = steg.infos; steg.AI=AI; steg.BodySkin = (skinselect) body; steg.EyesSkin = (eyesselect) eyes; steg.LodLevel = (lodselect) lod; steg.scale=scale;}
			break;
		case "Anky": if(!load){ anky=target[dino].GetComponent<anky_cs>(); name = "Ankylosaurus"; yadd=10; distance=20;
			infos=anky.infos; AI=anky.AI; body=anky.BodySkin.GetHashCode(); eyes=anky.EyesSkin.GetHashCode(); lod=anky.LodLevel.GetHashCode(); scale=anky.scale; load=true;}
			else if(gui) { infos = anky.infos; anky.AI=AI; anky.BodySkin = (skinselect) body; anky.EyesSkin = (eyesselect) eyes; anky.LodLevel = (lodselect) lod; anky.scale=scale;}
			break;
		case "Ptera": if(!load) { ptera=target[dino].GetComponent<ptera_cs>(); name = "Pteranodon"; yadd=10; distance=15;
				infos=ptera.infos; AI=ptera.AI; body=ptera.BodySkin.GetHashCode(); eyes=ptera.EyesSkin.GetHashCode(); lod=ptera.LodLevel.GetHashCode(); scale=ptera.scale; load=true;}
			else if(gui) { infos = ptera.infos; ptera.AI=AI; ptera.BodySkin = (skinselect) body; ptera.EyesSkin = (eyesselect) eyes; ptera.LodLevel = (lodselect) lod; ptera.scale=scale;}
			break;
		case "Pachy": if(!load) { pachy=target[dino].GetComponent<pachy_cs>(); name = "Pachycephalosaurus"; yadd=10; distance=15;
			infos=pachy.infos; AI=pachy.AI; body=pachy.BodySkin.GetHashCode(); eyes=pachy.EyesSkin.GetHashCode(); lod=pachy.LodLevel.GetHashCode(); scale=pachy.scale; load=true;}
			else if(gui) { infos = pachy.infos; pachy.AI=AI; pachy.BodySkin = (skinselect) body; pachy.EyesSkin = (eyesselect) eyes; pachy.LodLevel = (lodselect) lod; pachy.scale=scale;}
			break;
		case "Arge": if(!load) { arge=target[dino].GetComponent<arge_cs>(); name = "Argentinosaurus"; yadd=20; distance=45;
			infos=arge.infos; AI=arge.AI; body=arge.BodySkin.GetHashCode(); eyes=arge.EyesSkin.GetHashCode(); lod=arge.LodLevel.GetHashCode(); scale=arge.scale; load=true;}
			else if(gui) { infos = arge.infos; arge.AI=AI; arge.BodySkin = (skinselect) body; arge.EyesSkin = (eyesselect) eyes; arge.LodLevel = (lodselect) lod; arge.scale=scale;}
			break;
		*/
		}


		if(gui)
		{
			//Box
			GUI.Box (new Rect (0, 0, 240, Screen.height), name); GUI.color=Color.yellow;
			//Display buttons help
			GUI.Label(new Rect(5,20,Screen.width,Screen.height),"KEYS:");
			GUI.Label(new Rect(5,30,Screen.width,Screen.height),"---------------------------------------------------------");
			GUI.Label(new Rect(5,40,Screen.width,Screen.height),"Middle Mouse = Camera/Zoom");
			GUI.Label(new Rect(5,60,Screen.width,Screen.height),"Right Mouse = Spine move");
			GUI.Label(new Rect(5,80,Screen.width,Screen.height),"Left Mouse = Attack/Rise");
			GUI.Label(new Rect(5,100,Screen.width,Screen.height),"W,A,S,D = Moves");
			GUI.Label(new Rect(5,120,Screen.width,Screen.height),"LeftShift = Run/Landing/Fly Down");
			GUI.Label(new Rect(5,140,Screen.width,Screen.height),"Space = Steps/Jump/Takeoff/Fly Up");
			GUI.Label(new Rect(5,160,Screen.width,Screen.height),"E = Growl");
			GUI.Label(new Rect(5,180,Screen.width,Screen.height),"Num 1-9 = Idles/Eat/Drink/Sit/Sleep/Die");

			//Screen buttons
			GUI.Label(new Rect(5,220,Screen.width,Screen.height),"SCREEN:");
			GUI.Label(new Rect(5,230,Screen.width,Screen.height),"---------------------------------------------------------");

			GUI.Label(new Rect (5,250,55,20), "Fps "+fps); //Fps counter

			if (GUI.Button (new Rect (60,250,120,20), "Fullscreen")) //Full screen
				Screen.fullScreen = !Screen.fullScreen; 

			if (cammode == 0 && GUI.Button (new Rect (60, 270, 120, 20), "Cam Free")) cammode = 1; // free cam
			else if (cammode == 1 && GUI.Button (new Rect (60, 270, 120, 20), "Cam Chase")) cammode = 2; // locked cam
			else if (cammode == 2 && GUI.Button (new Rect (60, 270, 120, 20), "Cam Lock")) cammode = 0; // chase cam 

			if (wireframe == true && GUI.Button (new Rect (60,290,120,20), "Wireframe : ON"))  wireframe = false; //Wireframe mode
			else if(wireframe == false && GUI.Button (new Rect (60,290,120,20), "Wireframe : OFF")) wireframe = true;

			if (GUI.Button (new Rect (60,310,120,20), "Reset World")) Application.LoadLevel(0); //Reset

			//Model buttons
			GUI.Label(new Rect(5,360,Screen.width,Screen.height),"MODEL:");
			GUI.Label(new Rect(5,370,Screen.width,Screen.height),"---------------------------------------------------------");

			GUI.Label(new Rect(5,390,Screen.width,Screen.height),infos); //Model infos and lod
			if (GUI.Button (new Rect (100,390,120,20), lod==-1 ? "LOD_Auto":"LOD_"+lod.ToString())) { if(lod<2)lod++; else lod=-1; }

			GUI.Label(new Rect(5,430,Screen.width,Screen.height),"Dino AI"); //AI
			if (GUI.Button (new Rect (100,430,120,20), AI.ToString()))  { if(AI)AI = false; else AI=true; }

			GUI.Label(new Rect(5,450,Screen.width,Screen.height),"Body skin"); //Body skin
			if (GUI.Button (new Rect (100,450,120,20), "Skin "+body.ToString())) { if(body<2)body++; else body=0; }
	
			GUI.Label(new Rect(5,470,Screen.width,Screen.height),"Eyes skin"); //Eyes skin
			if (GUI.Button (new Rect (100,470,120,20), "Type "+eyes.ToString())) { if(eyes<15)eyes++; else eyes=0; }
	
			GUI.Label(new Rect(5,490,Screen.width,Screen.height),"Model Scale"); //Model scale
			scale=GUI.HorizontalSlider(new Rect (100,490,120,20),scale,0.05f,0.5f);
	
			/*
			if (size==0 && (GUI.Button (new Rect (100,490, 120,20), "Baby"))) size++; 
			else if (size==1 && (GUI.Button (new Rect (100,490, 120,20), "Young"))) size++; 
			else if (size==2  && (GUI.Button (new Rect (100,490, 120,20), "Young Adult"))) size++; 
			else if (size==3  && (GUI.Button (new Rect (100,490, 120,20), "Adult"))) size++; 
			else if (size==4  && (GUI.Button (new Rect (100,490, 120,20), "Giant"))) size=0;
			*/

			GUI.Label(new Rect(5,510,Screen.width,Screen.height),"Change Dino"); //Dino Select
			if ((GUI.Button (new Rect (100, 510, 120,20), target[dino].GetChild(0).name))) { if(dino<target.Length-1) dino++; else dino =0; load=false; }

		}

	}

	
	//***************************************************************************************
	//wireframe mode
	void OnPreRender()
	{
		if (wireframe == true) GL.wireframe = true;
		else GL.wireframe = false;
	}
	void OnPostRender()
	{
		GL.wireframe = false;
	}
	
	
}
