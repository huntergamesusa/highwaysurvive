#pragma strict
var punchClip:AudioClip;

var impact:GameObject;

var waterImpact:GameObject;
var waterSound:AudioClip;
//var explosionSound:AudioClip;

private var hits:int;
private var hits2:int;
//private var tntHit:int;
private var screams:int;
private var mainRigid:Rigidbody;
private var waterhit:int;
private var pongScreen:int;
private var gruntCount:int;
var isResetNeeded:boolean = false;

    public var crashSoft : AudioClip ;
    public var crashHard : AudioClip ;
    
    public var gruntSoft:AudioClip;
        public var gruntHard:AudioClip;
    public var gruntMedium:AudioClip;
    
//        public var gruntSoft : AudioClip ;
//    public var  gruntHard : AudioClip ;

    private var lowPitchRange :float = .75F;
    private var  highPitchRange : float = 1.5F;
    private var velToVol : float = .2F;
    private var velocityClipSplit : float = 10F;

function Start () {
mainRigid = GetComponent.<Rigidbody>();
pongScreen=0;
}

function Update () {

}

function OnCollisionEnter(coll:Collision){
//if(coll.gameObject.tag == "island" ){




if(GetComponent.<Rigidbody>().velocity.magnitude >=6){
hits++;

//GetComponent.<AudioSource>().PlayOneShot(punchClip);
if(hits<2){

if(coll.gameObject.name =="islandmesh"){
 var newimpact = Instantiate(impact,Vector3(coll.contacts[0].point.x,coll.contacts[0].point.y+.25,coll.contacts[0].point.z) ,Quaternion.identity);

newimpact.transform.parent = coll.transform;
}
}

}
if(coll.gameObject.tag == "street" ||coll.gameObject.tag == "enemy"){



        GetComponent.<AudioSource>().pitch = Random.Range (lowPitchRange,highPitchRange);
        var hitVol : float= coll.relativeVelocity.magnitude * velToVol;
        hitVol = Mathf.Clamp(hitVol,0f,.75f);
        if (coll.relativeVelocity.magnitude < velocityClipSplit && waterhit<1){

              GetComponent.<AudioSource>().PlayOneShot(crashSoft,hitVol);
//              lightGrunt(hitVol);
//              if(hits<2){
//              transform.GetChild(0).GetComponent.<AudioSource>().PlayOneShot(gruntSoft,hitVol);


//              }
              }
        else {
        if( waterhit<1){
              GetComponent.<AudioSource>().PlayOneShot(punchClip,hitVol);
              hardGrunt(hitVol);
              //TAKES PICTURE OF BIG IMPACT
				hits2++;
				if(hits2<2){

				if(PlayerPrefs.GetInt("selectedLevel")<2){

				snapPicture();
				}

				}
//              if(hits<2){
//               transform.GetChild(0).GetComponent.<AudioSource>().PlayOneShot(gruntHard,hitVol);
//               }
              }
       }


//}
if(coll.gameObject.tag == "island"){
StartCoroutine("resetGameDoubleCheck");
}
}



}

//function lightGrunt(gruntVol:float){
////transform.GetChild(0).GetComponent.<AudioSource>().PlayOneShot(gruntSoft,gruntVol);
//
//}
function hardGrunt(gruntVol:float){
gruntCount++;
if(gruntCount<2){
yield WaitForSeconds(.15);
var randomInt =Random.Range(0,3);
//print ("grunt" + randomInt);
if(randomInt==0){
transform.GetChild(0).GetComponent.<AudioSource>().PlayOneShot(gruntSoft,gruntVol);
}
if(randomInt==1){
transform.GetChild(0).GetComponent.<AudioSource>().PlayOneShot(gruntHard,gruntVol);
}
if(randomInt==2){
transform.GetChild(0).GetComponent.<AudioSource>().PlayOneShot(gruntMedium,gruntVol);

}
}
}

function OnTriggerEnter(colls:Collider){

if(colls.gameObject.tag == "scream"&& screams<1){
screams++;
//transform.GetChild(0).GetComponent.<AudioSource>().Play();
}

//if(colls.gameObject.tag == "tnt"&& tntHit<1){
//tntHit++;
//transform.GetChild(0).GetChild(1).GetComponent.<AudioSource>().Play();
//
//
//  print("hitTNT");
//
//}




if(colls.gameObject.tag == "waterTag"){
waterhit++;
if(waterhit<2){
 var newimpactWater = Instantiate(waterImpact,Vector3(transform.position.x,-21.286f,transform.position.z) ,Quaternion.identity);
       
//         var hitVol : float= colls.relativeVelocity.magnitude * velToVol;
  GetComponent.<AudioSource>().pitch =1;
              GetComponent.<AudioSource>().PlayOneShot(waterSound,.5);

    

}
}

}

function OnTriggerExit(colls:Collider){
if(colls.gameObject.tag =="buddyPongScreenColl"){
pongScreen++;
if(pongScreen<2&&PlayerPrefs.GetInt("selectedLevel")==2){
print("snapping water picture");
snapPicture();
}
}
}

//SNAPS IMAGE IMPACT
function snapPicture(){
yield WaitForSeconds(.2);
//var actionCam = GameObject.Find("CameraAction");
var actionCam = GameObject.Find("CameraAction_Screenshot");

//actionCam.SendMessage("newScreen");
//}
}

function stopresetGameDoubleCheck(){
StopCoroutine("resetGameDoubleCheck");
print("Coroutine for reset stopped");
}


function resetGameDoubleCheck(){
yield WaitForSeconds(4);
gameObject.SendMessage("doubleCheckEnd");
}

//yield WaitForSeconds (.1);



//}