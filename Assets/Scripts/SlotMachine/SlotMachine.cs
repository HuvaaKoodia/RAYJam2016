using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
    bool spinning;

    List<SkillID> SkillSet;

    public float SpinSpeed = 500f, SpinStopSpeed = 100f, spinSpeed;
    public bool Spinning { get { return spinSpeed > 0; } }
    public SkillID SelectedID { get; private set; }
    public GameObject newParent;
    public Image SlotImage;

	public AnimationCurve SlotStopAnimationCurve;

    private int SlotStackHeightThreshold, SlotStackHeight;

    public void SetItems(List<SkillID> skills)
    {
        SkillSet = skills;

        //add first 3 skills to the end of the list for proper looping
        SkillSet.Add(skills[0]);
        SkillSet.Add(skills[1]);
        SkillSet.Add(skills[2]);

        // spawn all pictures
        foreach (SkillID a in SkillSet)
        {
            Image ASkill = Instantiate(SlotImage, newParent.transform.position, Quaternion.identity) as Image;
            ASkill.sprite = SkillzDatabase.I.GetIcon(a);
            ASkill.transform.SetParent(newParent.transform, false);
        }

        SlotStackHeight = 100 * (skills.Count);
        SlotStackHeightThreshold = 100 * (skills.Count - 3);
    }

    public void ToggleSpin()
    {
        spinning = !spinning;
        spinSpeed = SpinSpeed;
		slotStopAnimation = false;
    }

	private float slotStopAnimationPercent = 0;
	private bool slotStopAnimation = false;
	private Vector3 slotStopAnimationStartPos, slotStopAnimationEndPos;

    // Update is called once per frame
    void Update()
    {
		if (slotStopAnimation) 
		{
			slotStopAnimationPercent += Time.deltaTime;

			if (slotStopAnimationPercent > 1) 
			{
				slotStopAnimationPercent = 1;
				slotStopAnimation = false;
			}

			newParent.transform.localPosition = slotStopAnimationStartPos + (slotStopAnimationEndPos - slotStopAnimationStartPos) * SlotStopAnimationCurve.Evaluate(slotStopAnimationPercent);
		}

        if (spinSpeed > 0) 
        {
            //decrease speed
            if (!spinning)
            {
                spinSpeed = Mathf.Lerp(spinSpeed, 0, Time.deltaTime * SpinStopSpeed);

                if (spinSpeed < 5f) 
                {
                    spinSpeed = 0;
                    //calculate selected skill ID when completely stopped based on the parent position.
					int selectedIndex = Mathf.FloorToInt(SkillSet.Count * (1 - (-newParent.transform.localPosition.y + 150f) / (float)SlotStackHeight));
                    
                    SelectedID = SkillSet[selectedIndex];

					slotStopAnimation = true;
					slotStopAnimationPercent = 0;
					slotStopAnimationStartPos = newParent.transform.localPosition;
					slotStopAnimationEndPos = Vector3.up * (1-((selectedIndex +2) / (float)SkillSet.Count)) * -SlotStackHeight;
                }
            }

            //move skill parent downwards
            var currentPosition = newParent.transform.localPosition;
            currentPosition.y -= spinSpeed * Time.deltaTime;

            //loop skill parent back up when below the threshold point
            if (currentPosition.y < -SlotStackHeightThreshold)
            {
                currentPosition.y = currentPosition.y + SlotStackHeightThreshold;
            }

            //set new position
            newParent.transform.localPosition = currentPosition;
        }
    }
}
