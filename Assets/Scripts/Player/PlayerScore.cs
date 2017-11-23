using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
	
	float _cutoffTime = 1;

	bool _accumulatingHitScore;
	int _accumulatedHitScore = 0;	
	float _accumulatingHitScoreTimecount = 0;

	bool _accumulatingKillScore;
	int _accumulatedKillScore = 0;	
	float _accumulatingKillScoreTimecount = 0;

	public static PlayerScore instance;

	// score list UI
	private ScoreList _scoreList;

	// Use this for initialization
	void Awake () {
		instance = this;
		_scoreList = GetComponentInChildren<ScoreList> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_accumulatingHitScore) {

			_accumulatingHitScoreTimecount += Time.deltaTime;

			if (_accumulatingHitScoreTimecount > _cutoffTime) {

				_accumulatingHitScore = false;
//				print ("_accumulatedHitScore " + _accumulatedHitScore);
				_scoreList.StopShowScore (ScoreText.ScoreType.Hit);
			}

		}

		if (_accumulatingKillScore) {

			_accumulatingKillScoreTimecount += Time.deltaTime;

			if (_accumulatingKillScoreTimecount > _cutoffTime) {

				_accumulatingKillScore = false;
				print ("_accumulatedKillScore " + _accumulatedKillScore);
				_scoreList.StopShowScore (ScoreText.ScoreType.Kill);
			}

		}
	}

	public void OnHitEnemy()
	{
		_accumulatingHitScore = true;
		_accumulatedHitScore += 10;
		_accumulatingHitScoreTimecount = 0;
		_scoreList.ShowScore (ScoreText.ScoreType.Hit,10);
	}

	public void OnKilledEnemy()
	{
		print ("OnKilledEnemy");
		_accumulatingKillScore = true;
		_accumulatedKillScore += 10;
		_accumulatingKillScoreTimecount = 0;
		_scoreList.ShowScore (ScoreText.ScoreType.Kill,100);
	}
}
