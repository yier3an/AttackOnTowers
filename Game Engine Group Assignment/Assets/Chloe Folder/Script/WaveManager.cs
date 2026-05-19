using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spawner for multiple enemies
public class WaveManager : MonoBehaviour
{
	[System.Serializable]
	public class WaveContent
	{
		// enemy included for the wave
		public Enemy enemy;
		public int maxCount;
		private int currCount = 0;

		public int GetCurrentCount()
		{
			return currCount;
		}

		public void AddCount()
		{
			currCount++;
		}
	}

	[System.Serializable]
	public class Wave
	{
		public string name;
		public List<WaveContent> waveContents;
		public float spawnRate;
	}

	private enum State
	{
		SPAWNING,
		WAITING,
		TIMING
	}
	private State state = State.TIMING;

	public List<Transform> spawnPoints;
	public List<Wave> waves;

	public float timeToNextWave;
	private float countDown;
	private float waitTime = 1.0f;
	private int nextWave = 0;

	public PlayerStatus playerStatus;

	void Start()
	{
		if (spawnPoints.Count == 0)
		{
			Debug.LogError("no spawn point");
		}

		// set count down timer
		countDown = timeToNextWave;
	}

	void Update()
	{
		if (state == State.WAITING)
		{
			// check whether enemies are still left
			if (!EnemyAlive())
			{
				// start new round
				WaveComplete();
			}
			else
			{
				// continue the game
				return;
			}
		}

		// spawn enemies when count down ends
		if (countDown <= 0)
		{
			if (state != State.SPAWNING)
			{
				// start spawning
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			// continue the countdown
			countDown -= Time.deltaTime;
		}
	}

	// other methods
	bool EnemyAlive()
	{
		// check if there are any enemies left every x second(s)
		waitTime -= Time.deltaTime;

		if (waitTime <= 0f)
		{
			waitTime = 1.0f;

			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}

		return true;
	}

	void WaveComplete()
	{
		// start waiting for next wave
		state = State.TIMING;
		countDown = timeToNextWave;

		if (nextWave + 1 > waves.Count - 1)
		{
			// Stage completes
			Debug.Log("All waves complete");
			if (playerStatus.getPlayerLives() > 0)
			{
				// switch to game win scene
				SceneManager.LoadScene("Game Win Scene");
			}
		}
		else
		{
			nextWave++;
		}
	}

	IEnumerator SpawnWave(Wave wave)
	{
		state = State.SPAWNING;

		// get total enemies to be spawned
		int no = 0;

		foreach (WaveContent wc in wave.waveContents)
		{
			no += wc.maxCount;
		}

		for (int i = 0; i < no; i++)
		{
			// randomise spawnpoint (if there are multiple)
			Transform sp = GetSpawnPoint();
			// spawn the enemy
			SpawnEnemy(wave.waveContents, sp);
			// time between each enemy spawn
			yield return new WaitForSeconds(wave.spawnRate);
		}

		state = State.WAITING;
	}

	void SpawnEnemy(List<WaveContent> waveContents, Transform sp)
	{
		bool MaxReached;
		Enemy enemy;

		// loop until random enemy has not reached max count
		do
		{
			// get random enemy
			int no = getRandomNo(waveContents.Count);
			WaveContent wc = waveContents[no];
			enemy = wc.enemy;

			// check if max count of enemy type reached
			if (wc.GetCurrentCount() < wc.maxCount)
			{
				wc.AddCount();
				MaxReached = false;
			}
			else
			{
				MaxReached = true;
			}

		} while (MaxReached);

		Vector3 pos;

		if (enemy.enemyType == Enemy.EnemyType.GROUND)
		{
			pos = sp.position;
		}
		else
		{
			// spawn above the spawn point
			float y = sp.position.y + 15.0f;
			pos = new Vector3(sp.position.x, y, sp.position.z);
		}
		// spawns the enemy object
		Instantiate(enemy, pos, Quaternion.identity);
	}

	Transform GetSpawnPoint()
	{
		return spawnPoints[getRandomNo(spawnPoints.Count)];
	}

	int getRandomNo(int n)
	{
		if (n <= 0)
		{
			return 0;
		}
		else
		{
			return Random.Range(0, n);
		}
	}
}